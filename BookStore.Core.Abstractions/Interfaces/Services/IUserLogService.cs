using BookStore.Core.Abstractions.Models.ApiResponses;
using BookStore.Core.Abstractions.Models.AuditLogs;

namespace BookStore.Core.Abstractions.Interfaces.Services
{
    public interface IUserLogService
    {
        Task<ApiResponse<IList<UserLog>>> GetUserLogsAsync(CancellationToken cancellationToken = default);
        Task SaveUserLog(UserLog userLog, CancellationToken cancellationToken = default);
    }
}
