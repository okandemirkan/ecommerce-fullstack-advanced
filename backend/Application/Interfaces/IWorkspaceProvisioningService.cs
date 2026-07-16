using Domain.Entities;

namespace Application.Interfaces
{
    public record DemoWorkspaceProvisioningResult(User Admin, DateTime ExpiresAt);

    public interface IWorkspaceProvisioningService
    {
        Task ProvisionRegisteredUserAsync(User user, CancellationToken cancellationToken);
        Task<DemoWorkspaceProvisioningResult> CreateDemoWorkspaceAsync(CancellationToken cancellationToken);
    }
}
