using Domain.Entities;

namespace Application.Interfaces
{
    public record DemoWorkspaceProvisioningResult(User Admin, DateTime ExpiresAt);

    public interface IWorkspaceProvisioningService
    {
        Task EnsureStorefrontWorkspaceAsync(CancellationToken cancellationToken);
        Task ProvisionRegisteredUserAsync(User user, CancellationToken cancellationToken);
        Task<DemoWorkspaceProvisioningResult> CreateDemoWorkspaceAsync(CancellationToken cancellationToken);
    }
}
