using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Services
{
    public class WorkspaceProvisioningService : IWorkspaceProvisioningService
    {
        private readonly ECommerceDbContext _context;
        private readonly IConfiguration _configuration;

        public WorkspaceProvisioningService(ECommerceDbContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task ProvisionRegisteredUserAsync(
            User user,
            CancellationToken cancellationToken)
        {
            await EnsureStorefrontWorkspaceAsync(cancellationToken);

            await using var transaction = await _context.Database
                .BeginTransactionAsync(cancellationToken);

            var workspaceId = await _context.Workspaces
                .Where(workspace => workspace.IsStorefront)
                .Select(workspace => workspace.Id)
                .SingleAsync(cancellationToken);

            user.AssignToWorkspace(workspaceId);
            foreach (var address in user.Addresses)
                address.AssignToWorkspace(workspaceId);

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }

        public async Task EnsureStorefrontWorkspaceAsync(CancellationToken cancellationToken)
        {
            if (await _context.Workspaces.AnyAsync(
                workspace => workspace.IsStorefront,
                cancellationToken))
            {
                return;
            }

            await using var transaction = await _context.Database
                .BeginTransactionAsync(cancellationToken);

            if (await _context.Workspaces.AnyAsync(
                workspace => workspace.IsStorefront,
                cancellationToken))
            {
                await transaction.CommitAsync(cancellationToken);
                return;
            }

            var workspace = Workspace.Create(isDemo: false, isStorefront: true);
            _context.Workspaces.Add(workspace);
            await _context.SaveChangesAsync(cancellationToken);

            await CloneTemplateDataAsync(workspace.Id, includeAdmin: false, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }

        public async Task<DemoWorkspaceProvisioningResult> CreateDemoWorkspaceAsync(
            CancellationToken cancellationToken)
        {
            var lifetimeMinutes = _configuration.GetValue<int?>("DemoWorkspace:LifetimeMinutes") ?? 120;
            var expiresAt = DateTime.UtcNow.AddMinutes(Math.Clamp(lifetimeMinutes, 15, 1440));

            await using var transaction = await _context.Database
                .BeginTransactionAsync(cancellationToken);

            var workspace = Workspace.Create(isDemo: true, expiresAt);
            _context.Workspaces.Add(workspace);
            await _context.SaveChangesAsync(cancellationToken);

            var users = await CloneTemplateDataAsync(
                workspace.Id,
                includeAdmin: true,
                cancellationToken);

            var admin = users.Values.FirstOrDefault(u => u.RoleId == 1)
                ?? throw new InvalidOperationException("The workspace template does not contain an administrator.");

            await _context.Entry(admin).Reference(u => u.Role).LoadAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return new DemoWorkspaceProvisioningResult(admin, expiresAt);
        }

        private async Task<Dictionary<int, User>> CloneTemplateDataAsync(
            Guid workspaceId,
            bool includeAdmin,
            CancellationToken cancellationToken)
        {
            var categoryMap = await CloneCategoriesAsync(workspaceId, cancellationToken);
            var productMap = await CloneProductsAsync(workspaceId, categoryMap, cancellationToken);
            var userMap = await CloneUsersAsync(workspaceId, includeAdmin, cancellationToken);

            await CloneOrdersAsync(workspaceId, userMap, productMap, cancellationToken);
            await CloneReviewsAsync(workspaceId, userMap, productMap, cancellationToken);
            await CloneCartItemsAsync(workspaceId, userMap, productMap, cancellationToken);

            return userMap;
        }

        private async Task<Dictionary<int, Category>> CloneCategoriesAsync(
            Guid workspaceId,
            CancellationToken cancellationToken)
        {
            var templates = await _context.Categories.IgnoreQueryFilters().AsNoTracking()
                .Where(c => c.WorkspaceId == null && !c.IsDeleted)
                .OrderBy(c => c.Id)
                .ToListAsync(cancellationToken);

            var map = templates.ToDictionary(
                template => template.Id,
                template =>
                {
                    var copy = Category.AddCategory(template.CategoryName);
                    copy.AssignToWorkspace(workspaceId);
                    return copy;
                });

            _context.Categories.AddRange(map.Values);
            await _context.SaveChangesAsync(cancellationToken);
            return map;
        }

        private async Task<Dictionary<int, Product>> CloneProductsAsync(
            Guid workspaceId,
            IReadOnlyDictionary<int, Category> categories,
            CancellationToken cancellationToken)
        {
            var templates = await _context.Products.IgnoreQueryFilters().AsNoTracking()
                .Where(p => p.WorkspaceId == null && !p.IsDeleted)
                .OrderBy(p => p.Id)
                .ToListAsync(cancellationToken);

            var map = new Dictionary<int, Product>();
            foreach (var template in templates)
            {
                if (!categories.TryGetValue(template.CategoryId, out var category))
                    continue;

                var copy = Product.AddProduct(
                    template.ProductName,
                    category.Id,
                    template.Description,
                    template.ImageUrl,
                    template.Price,
                    template.Stock);
                copy.AssignToWorkspace(workspaceId);
                map.Add(template.Id, copy);
            }

            _context.Products.AddRange(map.Values);
            await _context.SaveChangesAsync(cancellationToken);
            return map;
        }

        private async Task<Dictionary<int, User>> CloneUsersAsync(
            Guid workspaceId,
            bool includeAdmin,
            CancellationToken cancellationToken)
        {
            var users = await _context.Users.IgnoreQueryFilters().AsNoTracking()
                .Where(u => u.WorkspaceId == null && !u.IsDeleted && (includeAdmin || u.RoleId != 1))
                .OrderBy(u => u.Id)
                .ToListAsync(cancellationToken);
            var addresses = await _context.Addresses.IgnoreQueryFilters().AsNoTracking()
                .Where(a => a.WorkspaceId == null && !a.IsDeleted)
                .ToListAsync(cancellationToken);

            var addressesByUser = addresses.ToLookup(a => a.UserId);
            var map = new Dictionary<int, User>();
            var demoUserNumber = 1;
            foreach (var template in users)
            {
                var email = includeAdmin
                    ? BuildDemoEmail(demoUserNumber)
                    : BuildWorkspaceEmail(template.Id, workspaceId);
                var phoneNumber = includeAdmin
                    ? BuildDemoPhone(demoUserNumber)
                    : BuildWorkspacePhone(template.Id, workspaceId);

                var copy = User.CreateWorkspaceCopy(
                    template.Username,
                    email,
                    template.PasswordHash,
                    phoneNumber,
                    template.RoleId,
                    workspaceId);

                foreach (var addressTemplate in addressesByUser[template.Id])
                {
                    var address = Address.CreateAddress(
                        addressTemplate.City,
                        addressTemplate.AddressType,
                        addressTemplate.District,
                        addressTemplate.FullAddress,
                        addressTemplate.ZipCode);
                    address.AssignToWorkspace(workspaceId);
                    copy.AddAddress(address);
                }

                map.Add(template.Id, copy);
                demoUserNumber++;
            }

            _context.Users.AddRange(map.Values);
            await _context.SaveChangesAsync(cancellationToken);
            return map;
        }

        private async Task CloneOrdersAsync(
            Guid workspaceId,
            IReadOnlyDictionary<int, User> users,
            IReadOnlyDictionary<int, Product> products,
            CancellationToken cancellationToken)
        {
            var orderTemplates = await _context.Orders.IgnoreQueryFilters().AsNoTracking()
                .Where(o => o.WorkspaceId == null && !o.IsDeleted)
                .OrderBy(o => o.Id)
                .ToListAsync(cancellationToken);
            var itemTemplates = await _context.OrderItems.IgnoreQueryFilters().AsNoTracking()
                .Where(i => i.WorkspaceId == null && !i.IsDeleted)
                .ToListAsync(cancellationToken);
            var itemsByOrder = itemTemplates.ToLookup(i => i.OrderId);

            foreach (var template in orderTemplates)
            {
                if (!users.TryGetValue(template.UserId, out var user))
                    continue;

                var order = Order.CreateWorkspaceCopy(
                    user.Id,
                    template.ShippingAddress,
                    template.TotalPrice,
                    template.OrderStatus,
                    workspaceId);

                foreach (var itemTemplate in itemsByOrder[template.Id])
                {
                    if (!products.TryGetValue(itemTemplate.ProductId, out var product))
                        continue;

                    var item = OrderItem.CreateWorkspaceCopy(
                        product.Id,
                        itemTemplate.ProductName,
                        itemTemplate.ImageUrl,
                        itemTemplate.Price,
                        itemTemplate.Quantity,
                        workspaceId);
                    order.AddWorkspaceItem(item);
                }

                _context.Orders.Add(order);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task CloneReviewsAsync(
            Guid workspaceId,
            IReadOnlyDictionary<int, User> users,
            IReadOnlyDictionary<int, Product> products,
            CancellationToken cancellationToken)
        {
            var templates = await _context.Reviews.IgnoreQueryFilters().AsNoTracking()
                .Where(r => r.WorkspaceId == null && !r.IsDeleted)
                .ToListAsync(cancellationToken);

            foreach (var template in templates)
            {
                if (!users.TryGetValue(template.UserId, out var user) ||
                    !products.TryGetValue(template.ProductId, out var product))
                    continue;

                var review = Review.CreateReview(
                    user.Id,
                    product.Id,
                    template.Comment,
                    template.Rating);
                review.AssignToWorkspace(workspaceId);
                _context.Reviews.Add(review);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task CloneCartItemsAsync(
            Guid workspaceId,
            IReadOnlyDictionary<int, User> users,
            IReadOnlyDictionary<int, Product> products,
            CancellationToken cancellationToken)
        {
            var templates = await _context.CartItems.IgnoreQueryFilters().AsNoTracking()
                .Where(c => c.WorkspaceId == null && !c.IsDeleted)
                .ToListAsync(cancellationToken);

            foreach (var template in templates)
            {
                if (!users.TryGetValue(template.UserId, out var user) ||
                    !products.TryGetValue(template.ProductId, out var product))
                    continue;

                var cartItem = CartItem.CreateCart(user.Id, product, template.Quantity);
                cartItem.AssignToWorkspace(workspaceId);
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        private static string BuildDemoEmail(int demoUserNumber)
        {
            return $"demo-{demoUserNumber}@example.com";
        }

        private static string BuildDemoPhone(int demoUserNumber)
        {
            return $"555{demoUserNumber:D7}";
        }

        private static string BuildWorkspaceEmail(int sourceUserId, Guid workspaceId)
        {
            return $"workspace-{sourceUserId}-{workspaceId:N}@example.local";
        }

        private static string BuildWorkspacePhone(int sourceUserId, Guid workspaceId)
        {
            var input = Encoding.UTF8.GetBytes($"{workspaceId:N}:{sourceUserId}");
            var hash = SHA256.HashData(input);
            var number = BitConverter.ToUInt32(hash, 0) % 1_000_000_000;
            return $"5{number:D9}";
        }
    }
}
