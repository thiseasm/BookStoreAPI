using BookStore.Core.Abstractions.Events;
using BookStore.Core.Abstractions.Interfaces.Services;
using BookStore.Core.Abstractions.Models.AuditLogs;
using MediatR;

namespace BookStore.Core.Handlers
{
    public class UserRoleUpdatedHandler(IUserLogService logService) : IRequestHandler<UserRolesUpdatedEvent>
    {
        public async Task Handle(UserRolesUpdatedEvent notification, CancellationToken cancellationToken = default)
        {
            var log = new UserLog
            {
                Timestamp = notification.Timestamp,
                Action = "Roles Updated",
                EntityId = notification.UserId,
                PreviousState = string.Join(",", notification.RolesRemoved),
                NextState = string.Join(",", notification.RolesAdded),
            };

            await logService.SaveUserLog(log, cancellationToken);
        }
    }
}
