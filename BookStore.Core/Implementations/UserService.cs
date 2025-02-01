using BookStore.Core.Abstractions.Interfaces;
using BookStore.Core.Abstractions.Models.ApiResponses;
using BookStore.Core.Abstractions.Models.Users;
using BookStore.Core.Extensions;
using BookStore.Infrastructure.Data;

namespace BookStore.Core.Implementations
{
    public class UserService(BookStoreContext dbContext) : IUserService
    {
        public async Task<ApiResponse<CreateUserResponse>> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var userDto = request.ToDto();

            await dbContext.Users.AddAsync(userDto, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            
            if(userDto.Id == 0)
            {
                return ApiResponse<CreateUserResponse>.InternalError("Failed to create user");
            }


            return ApiResponse<CreateUserResponse>.Created(new CreateUserResponse 
            { 
                Id = userDto.Id 
            });
        }
    }
}
