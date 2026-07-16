using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Domain.Entities;
using System.Reflection;
namespace Infrastructure.Persistence
{
    public class ECommerceDbContext : DbContext
    {
        private readonly IWorkspaceContext _workspaceContext;
        public ECommerceDbContext(DbContextOptions options, IWorkspaceContext workspaceContext) : base(options)
        {
            _workspaceContext = workspaceContext;
        }
        public Guid? CurrentWorkspaceId => _workspaceContext.WorkspaceId;
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem>OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Review>Reviews { get; set; }
        public DbSet<Category> Categories { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            ConfigureWorkspaceOwnership(modelBuilder);
            ConfigureWorkspaceFilters(modelBuilder);
        }

        public override int SaveChanges()
        {
            AssignCurrentWorkspace();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AssignCurrentWorkspace();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AssignCurrentWorkspace()
        {
            if (!CurrentWorkspaceId.HasValue)
                return;

            foreach (var entry in ChangeTracker.Entries<IWorkspaceEntity>()
                .Where(e => e.State == EntityState.Added && !e.Entity.WorkspaceId.HasValue))
            {
                entry.Entity.AssignToWorkspace(CurrentWorkspaceId.Value);
            }
        }

        private static void ConfigureWorkspaceOwnership(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne<Workspace>().WithMany()
                .HasForeignKey(e => e.WorkspaceId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Category>().HasOne<Workspace>().WithMany()
                .HasForeignKey(e => e.WorkspaceId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>().HasOne<Workspace>().WithMany()
                .HasForeignKey(e => e.WorkspaceId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Address>().HasOne<Workspace>().WithMany()
                .HasForeignKey(e => e.WorkspaceId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Order>().HasOne<Workspace>().WithMany()
                .HasForeignKey(e => e.WorkspaceId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<OrderItem>().HasOne<Workspace>().WithMany()
                .HasForeignKey(e => e.WorkspaceId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Review>().HasOne<Workspace>().WithMany()
                .HasForeignKey(e => e.WorkspaceId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CartItem>().HasOne<Workspace>().WithMany()
                .HasForeignKey(e => e.WorkspaceId).OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureWorkspaceFilters(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(e =>
                !e.IsDeleted && e.WorkspaceId == CurrentWorkspaceId);
            modelBuilder.Entity<Category>().HasQueryFilter(e =>
                !e.IsDeleted && e.WorkspaceId == CurrentWorkspaceId);
            modelBuilder.Entity<Product>().HasQueryFilter(e =>
                !e.IsDeleted && e.WorkspaceId == CurrentWorkspaceId);
            modelBuilder.Entity<Address>().HasQueryFilter(e =>
                !e.IsDeleted && e.WorkspaceId == CurrentWorkspaceId);
            modelBuilder.Entity<Order>().HasQueryFilter(e =>
                !e.IsDeleted && e.WorkspaceId == CurrentWorkspaceId);
            modelBuilder.Entity<OrderItem>().HasQueryFilter(e =>
                !e.IsDeleted && e.WorkspaceId == CurrentWorkspaceId);
            modelBuilder.Entity<Review>().HasQueryFilter(e =>
                !e.IsDeleted && e.WorkspaceId == CurrentWorkspaceId);
            modelBuilder.Entity<CartItem>().HasQueryFilter(e =>
                !e.IsDeleted && e.WorkspaceId == CurrentWorkspaceId);
        }

    }
}
