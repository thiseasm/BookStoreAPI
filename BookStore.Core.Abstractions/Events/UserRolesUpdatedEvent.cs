using MediatR;

namespace BookStore.Core.Abstractions.Events
{
    public record UserRolesUpdatedEvent(int UserId, HashSet<int> RolesRemoved, HashSet<int> RolesAdded, DateTime Timestamp) : IRequest;
}
