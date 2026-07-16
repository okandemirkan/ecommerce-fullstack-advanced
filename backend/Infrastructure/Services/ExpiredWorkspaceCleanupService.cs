using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class ExpiredWorkspaceCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ExpiredWorkspaceCleanupService> _logger;

        public ExpiredWorkspaceCleanupService(
            IServiceScopeFactory scopeFactory,
            ILogger<ExpiredWorkspaceCleanupService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromMinutes(10));

            do
            {
                try
                {
                    await RemoveExpiredWorkspacesAsync(stoppingToken);
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Expired demo workspaces could not be removed.");
                }
            }
            while (await timer.WaitForNextTickAsync(stoppingToken));
        }

        private async Task RemoveExpiredWorkspacesAsync(CancellationToken cancellationToken)
        {
            await using var scope = _scopeFactory.CreateAsyncScope();
            var context = scope.ServiceProvider.GetRequiredService<ECommerceDbContext>();
            var now = DateTime.UtcNow;

            var expired = await context.Workspaces
                .Where(w => w.IsDemo && w.ExpiresAt <= now)
                .ToListAsync(cancellationToken);

            if (expired.Count == 0)
                return;

            context.Workspaces.RemoveRange(expired);
            await context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Removed {WorkspaceCount} expired demo workspaces.", expired.Count);
        }
    }
}
