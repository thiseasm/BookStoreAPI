using BookStore.Core.Abstractions.Interfaces;
using BookStore.Core.Abstractions.Models.Users;
using BookStore.Core.Extensions;
using BookStore.Data.Abstractions.Context;

namespace BookStore.Core.Implementations
{
    public class UserService(BookStoreContext dbContext) : IUserService
    {
        public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var userDto = request.ToDto();

            await dbContext.Users.AddAsync(userDto, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateUserResponse { Id = userDto.Id };
        }
    }
}
