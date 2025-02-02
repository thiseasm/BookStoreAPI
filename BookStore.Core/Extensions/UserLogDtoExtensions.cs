using BookStore.Core.Abstractions.Models.AuditLogs;
using BookStore.Infrastructure.DTOs;

namespace BookStore.Core.Extensions
{
    public static class UserLogDtoExtensions
    {
        public static UserLog ToDomain(this UserLogDto userLog)
            => new()
            {
                Timestamp = userLog.Timestamp,
                Action = userLog.Action,
                EntityId = userLog.EntityId,
                PreviousState = userLog.PreviousState,
                NextState = userLog.NextState
            };
    }
}
