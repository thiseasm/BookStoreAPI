using BookStore.Core.Abstractions.Interfaces;
using BookStore.Core.Abstractions.Models;
using BookStore.Core.Abstractions.Models.ApiResponses;
using BookStore.Core.Abstractions.Models.Users;
using BookStore.Core.Extensions;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Core.Implementations
{
    public class UserService(BookStoreContext dbContext) : IUserService
    {
        public async Task<ApiResponse<IList<User>>> GetUsersAsync(CancellationToken cancellationToken)
        {
            var result = await dbContext.Users
                .Include(user => user.Roles)
                .AsNoTracking()
                .Select(user => user.ToDomain())
                .ToListAsync(cancellationToken);

            return ApiResponse<IList<User>>.Ok(result);
        }

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

        public async Task<ApiResponse<string>> UpdateUserRolesAsync(int id, UpdateUserRolesRequest request, CancellationToken cancellationToken)
        {
            var userDto = await dbContext.Users
                .Include(user => user.Roles)
                .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
            if (userDto == null)
            {
                return ApiResponse<string>.NotFound($"User with ID: {id} not found");
            }

            if(request.RoleIds.Count == 0)
            {
                userDto.Roles.Clear();
                await dbContext.SaveChangesAsync(cancellationToken);
                return ApiResponse<string>.Ok($"Roles for User with ID: {id} have been cleared");
            }

            var roleDtos = await dbContext.Roles
                .Where(r => request.RoleIds.Contains(r.Id))
                .ToListAsync(cancellationToken);
            
            var rolesNotFound = request.RoleIds.Except(roleDtos.Select(r => r.Id)).ToList();

            if(rolesNotFound.Count > 0)
            {
                return ApiResponse<string>.NotFound($"Roles with IDs: {string.Join(", ", rolesNotFound)} not found");
            }

            userDto.Roles.Clear();
            foreach (var role in roleDtos)
            {
                userDto.Roles.Add(role);
            }
            await dbContext.SaveChangesAsync(cancellationToken);

            return ApiResponse<string>.Ok($"Roles for User with ID: {id} have been updated");

        }
    }
}
