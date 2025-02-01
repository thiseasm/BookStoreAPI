using BookStore.Core.Abstractions.Models.ApiResponses;
using BookStore.Core.Abstractions.Models.Users;

namespace BookStore.Core.Abstractions.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<CreateUserResponse>> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default);
    }
}
