using BookStore.Core.Abstractions.Events;
using BookStore.Core.Abstractions.Interfaces.Services;
using BookStore.Core.Abstractions.Models.AuditLogs;
using MediatR;

namespace BookStore.Core.Handlers
{
    public class UserRoleUpdatedHandler(IUserLogService logService) : IRequestHandler<UserRoleUpdatedEvent>
    {
        public async Task Handle(UserRoleUpdatedEvent notification, CancellationToken cancellationToken = default)
        {
            var log = new UserLog
            {
                Timestamp = notification.Timestamp,
                Action = "UserCreated",
                EntityId = notification.UserId,
                PreviousState = string.Join(",", notification.RolesRemoved),
                NextState = string.Join(",", notification.RolesAdded),
            };

            await logService.SaveUserLog(log, cancellationToken);
        }
    }
}
