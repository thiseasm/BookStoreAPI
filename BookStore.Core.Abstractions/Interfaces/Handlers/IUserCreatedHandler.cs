using BookStore.Core.Abstractions.Events;

namespace BookStore.Core.Abstractions.Interfaces.Handlers
{
    public interface IUserCreatedHandler
    {
        Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken = default);
    }
}
