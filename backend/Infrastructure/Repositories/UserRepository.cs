using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Application.Pagination;
namespace Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly ECommerceDbContext _context;
        public UserRepository(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<PagedList<User>> GetAllUsersPaged(int pageNumber,int pageSize)
        {
            var totalCount = await _context.Users.CountAsync();

            var users = await _context.Users.OrderBy(user => user.Id)
                .Skip((pageNumber-1)*pageSize).Take(pageSize)
                .Include(u=>u.Addresses).Include(u=>u.Role).ToListAsync();

            return new PagedList<User>(users,totalCount,pageNumber,pageSize);
        }

        public async Task<PagedList<User>> GetSoftDeletedUsersPaged(int pageNumber,int pageSize)
        {
            var query = _context.Users.IgnoreQueryFilters().Where(u =>
                    u.IsDeleted && u.WorkspaceId == _context.CurrentWorkspaceId)
                .Include(u=>u.Addresses).Include(u=>u.Role);

            var totalCount = await query.CountAsync();

            var users = await query.OrderBy(user => user.Id)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedList<User>(users,totalCount, pageNumber, pageSize);
        }
        public async Task<User?> GetUserById(int userId)
        {
            var user = await _context.Users.Include(c => c.Addresses).Include(c => c.Role).IgnoreQueryFilters()
                .SingleOrDefaultAsync(c => c.Id == userId &&
                    c.WorkspaceId == _context.CurrentWorkspaceId);
            return user;
        }
        public async Task<User?>GetAnyUserById(int userId)
        {
            var user = await _context.Users.IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Id == userId &&
                    u.WorkspaceId == _context.CurrentWorkspaceId);
            return user;
        }
        public async Task<User?>GetAnyUserByPhoneNumber(string phoneNumber)
        {
            var user = await _context.Users.IgnoreQueryFilters().Include(u => u.Role)
                .Include(u=>u.Addresses).FirstOrDefaultAsync(u=>u.PhoneNumber == phoneNumber &&
                    u.WorkspaceId == _context.CurrentWorkspaceId);
            return user;
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _context.Users.IgnoreQueryFilters().Include(u=>u.Role)
                .FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted && u.WorkspaceId != null &&
                    _context.Workspaces.Any(w => w.Id == u.WorkspaceId && !w.IsDemo));
            return user;
        }
        public async Task<User?>GetAnyUserByEmail(string email)
        {
            var user = await _context.Users.Include(u => u.Role).Include(u=>u.Addresses).IgnoreQueryFilters().
                FirstOrDefaultAsync(u => u.Email == email &&
                    u.WorkspaceId == _context.CurrentWorkspaceId);
            return user;
        }

        public async Task<PagedList<User>> SearchUserByName(string userName, int pageNumber, int pageSize)
        {
            var normalizedName = userName.Trim().ToLower();

            var query =  _context.Users.Include(u => u.Role).Include(u=>u.Addresses).IgnoreQueryFilters()
                .Where(u => u.WorkspaceId == _context.CurrentWorkspaceId &&
                    u.Username.ToLower().Contains(normalizedName));

            var totalCount = await query.CountAsync();

            var users = await query.OrderBy(user => user.Id)
                .Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();

            return new PagedList<User>(users,totalCount,pageNumber,pageSize);
        }
        public async Task AddUser(User newCustomer)
        {
            await _context.Users.AddAsync(newCustomer);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUser(User updatedCustomer)
        {
            _context.Users.Update(updatedCustomer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByAddressId(int addressId)
        {
            return await _context.Users.Include(c => c.Addresses).
                FirstOrDefaultAsync(c => c.Addresses.Any(
                    a => a.Id == addressId));
        }
        public async Task<bool> IsUserActive(int userId)
        {
            return await _context.Users.AnyAsync(u=>u.Id == userId);
        }
        public async Task<bool> IsEmailExist(string email,int currentId = 0)
        {
            var result = await _context.Users.IgnoreQueryFilters()
                .AnyAsync(c=>c.Email ==  email && c.Id != currentId);
            return result;
        }
        public async Task<bool> IsPhoneNumberExist(string phoneNumber, int currentId = 0)
        {
            var result = await _context.Users.IgnoreQueryFilters()
                .AnyAsync(c => c.PhoneNumber == phoneNumber && c.Id != currentId);
            return result;
        }

    }
}
