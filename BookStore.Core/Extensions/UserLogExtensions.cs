using BookStore.Core.Abstractions.Models.AuditLogs;
using BookStore.Infrastructure.DTOs;

namespace BookStore.Core.Extensions
{
    public static class UserLogExtensions
    {
        public static UserLogDto ToDto(this UserLog userLog)
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
