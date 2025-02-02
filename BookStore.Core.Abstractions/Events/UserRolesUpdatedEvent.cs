using MediatR;

namespace BookStore.Core.Abstractions.Events
{
    public record UserRoleUpdatedEvent(int UserId, List<int> RolesRemoved, List<int> RolesAdded, DateTime Timestamp) : INotification;
}
