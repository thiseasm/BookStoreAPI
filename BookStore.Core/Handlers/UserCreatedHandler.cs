using BookStore.Core.Abstractions.Events;
using BookStore.Core.Abstractions.Interfaces.Services;
using BookStore.Core.Abstractions.Models.AuditLogs;
using MediatR;

namespace BookStore.Core.Handlers
{
    public class UserCreatedHandler(IUserLogService logService) : IRequestHandler<UserCreatedEvent>
    {
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            var log = new UserLog
            {
                Timestamp = notification.Timestamp,
                Action = "Created",
                EntityId = notification.CreatedUser.Id,
                PreviousState = string.Empty,
                NextState = notification.CreatedUser.ToString()
            };

            await logService.SaveUserLog(log, cancellationToken);
        }
    }
}
