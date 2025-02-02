using BookStore.Core.Abstractions.Events;

namespace BookStore.Core.Abstractions.Interfaces.Handlers
{
    public interface IUserRoleUpdatedHandler
    {
        Task Handle(UserRoleUpdatedEvent notification, CancellationToken cancellationToken = default);
    }
}
