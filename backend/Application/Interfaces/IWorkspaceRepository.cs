namespace Application.Interfaces
{
    public interface IWorkspaceRepository
    {
        Task<bool> IsDemoAsync(Guid workspaceId, CancellationToken cancellationToken = default);
        Task<bool> DeleteDemoAsync(Guid workspaceId, CancellationToken cancellationToken = default);
    }
}
