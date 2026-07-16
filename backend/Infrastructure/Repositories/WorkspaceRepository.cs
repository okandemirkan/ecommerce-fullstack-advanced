using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly ECommerceDbContext _context;

        public WorkspaceRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public Task<bool> IsDemoAsync(Guid workspaceId, CancellationToken cancellationToken = default)
        {
            return _context.Workspaces.AnyAsync(
                workspace => workspace.Id == workspaceId && workspace.IsDemo,
                cancellationToken);
        }
    }
}
