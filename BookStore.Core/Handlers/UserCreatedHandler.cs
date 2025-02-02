using BookStore.Core.Abstractions.Events;
using BookStore.Core.Abstractions.Interfaces.Services;
using BookStore.Core.Abstractions.Models.AuditLogs;
using MediatR;
using System.Text.Json;

namespace BookStore.Core.Handlers
{
    public class UserCreatedHandler(IUserLogService logService) : IRequestHandler<UserCreatedEvent>
    {
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            var log = new UserLog
            {
                Timestamp = notification.Timestamp,
                Action = "UserCreated",
                EntityId = notification.CreatedUser.Id,
                PreviousState = string.Empty,
                NextState = JsonSerializer.Serialize(notification.CreatedUser)
            };

            await logService.SaveUserLog(log, cancellationToken);
        }
    }
}
