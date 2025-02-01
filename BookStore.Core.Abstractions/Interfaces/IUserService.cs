using BookStore.Core.Abstractions.Models.Users;

namespace BookStore.Core.Abstractions.Interfaces
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default);
    }
}
