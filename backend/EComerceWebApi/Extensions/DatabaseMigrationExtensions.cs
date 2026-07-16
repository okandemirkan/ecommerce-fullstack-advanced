using Infrastructure.Persistence;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EComerceWebApi.Extensions;

public static class DatabaseMigrationExtensions
{
    public static async Task ApplyDatabaseMigrationsAsync(this WebApplication app)
    {
        if (!app.Configuration.GetValue<bool>("Database:ApplyMigrations"))
            return;

        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ECommerceDbContext>();

        await dbContext.Database.MigrateAsync();

        var workspaceProvisioningService = scope.ServiceProvider
            .GetRequiredService<IWorkspaceProvisioningService>();
        await workspaceProvisioningService.EnsureStorefrontWorkspaceAsync(CancellationToken.None);
    }
}
