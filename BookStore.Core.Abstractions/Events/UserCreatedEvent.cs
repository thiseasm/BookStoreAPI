using BookStore.Core.Abstractions.Models.Users;
using MediatR;

namespace BookStore.Core.Abstractions.Events
{
    public record UserCreatedEvent(User CreatedUser, DateTime Timestamp) : INotification;
}
