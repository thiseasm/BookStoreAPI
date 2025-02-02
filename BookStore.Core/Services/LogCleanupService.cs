using BookStore.Core.Abstractions.Configurations;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookStore.Core.Services
{
    public class LogCleanupService(ILogger<LogCleanupService> logger, IOptions<LogCleanupConfig> config, IServiceScopeFactory scopeFactory) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Log Cleanup Service is starting.");

            if(config == null)
            {
               logger.LogError("LogCleanupConfig is not configured.");
               return;
            }

            var cleanupSettings = config.Value;
            if (cleanupSettings.DaysToKeepLogs <= 0 || cleanupSettings.CleanupIntervalMinutes <= 0)
            {
                throw new InvalidOperationException("Invalid configuration for LogCleanupService.");
            } 

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = scopeFactory.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<BookStoreContext>();

                    var cutOffDate = DateTime.UtcNow.AddDays(-cleanupSettings.DaysToKeepLogs);

                    int deletedLogs = await dbContext.UserLogs
                        .Where(log => log.Timestamp < cutOffDate)
                        .ExecuteDeleteAsync(cancellationToken);

                    logger.LogInformation($"Deleted {deletedLogs} old audit logs.");

                    await Task.Delay(TimeSpan.FromMinutes(cleanupSettings.CleanupIntervalMinutes), cancellationToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error occurred while cleaning up audit logs.");
                }
            }

            logger.LogInformation("Log Cleanup Service is stopping.");
        }
    }
}
