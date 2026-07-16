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

        public async Task<bool> DeleteDemoAsync(
            Guid workspaceId,
            CancellationToken cancellationToken = default)
        {
            var workspace = await _context.Workspaces.SingleOrDefaultAsync(
                candidate => candidate.Id == workspaceId && candidate.IsDemo,
                cancellationToken);

            if (workspace is null)
                return false;

            _context.Workspaces.Remove(workspace);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
