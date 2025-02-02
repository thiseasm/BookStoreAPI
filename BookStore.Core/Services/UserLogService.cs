using BookStore.Core.Abstractions.Interfaces.Services;
using BookStore.Core.Abstractions.Models.ApiResponses;
using BookStore.Core.Abstractions.Models.AuditLogs;
using BookStore.Core.Extensions;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.Core.Services
{
    public class UserLogService(ILogger<UserLogService> logger, BookStoreContext dbContext) : IUserLogService
    {
        public async Task<ApiResponse<IList<UserLog>>> GetUserLogsAsync(CancellationToken cancellationToken)
        {
            try
            {
                var result = await dbContext.UserLogs
                    .OrderByDescending(log => log.Timestamp)
                    .Select(log => log.ToDomain())
                    .ToListAsync(cancellationToken);

                return ApiResponse<IList<UserLog>>.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while retrieving Audit Logs for Entity: User");
                return ApiResponse<IList<UserLog>>.InternalError("An unexpected error occurred.");
            }
        }

        public async Task SaveUserLog(UserLog userLog, CancellationToken cancellationToken = default)
        {
            try
            {
                dbContext.UserLogs.Add(userLog.ToDto());
                await dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to save User Log for Action:{Action}, UserId: {EntityId}", userLog.Action, userLog.EntityId);
            }

        }
    }
}
