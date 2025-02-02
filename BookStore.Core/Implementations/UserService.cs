using BookStore.Core.Abstractions.Interfaces;
using BookStore.Core.Abstractions.Models;
using BookStore.Core.Abstractions.Models.ApiResponses;
using BookStore.Core.Abstractions.Models.Users;
using BookStore.Core.Extensions;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookStore.Core.Implementations
{
    public class UserService(ILogger<UserService> logger,BookStoreContext dbContext) : IUserService
    {
        public async Task<ApiResponse<User>> GetUserByIdAsync(int userId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await dbContext.Users
                    .Include(user => user.Roles)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);

                return result != null
                    ? ApiResponse<User>.Ok(result.ToDomain())
                    : ApiResponse<User>.NotFound($"User with ID: {userId} not found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while retrieving User ID: {UserId}", userId);
                return ApiResponse<User>.InternalError("An unexpected error occurred.");
            }
        }


        public async Task<ApiResponse<IList<User>>> GetUsersAsync(CancellationToken cancellationToken)
        {
            try
            {
                var result = await dbContext.Users
                .Include(user => user.Roles)
                .AsNoTracking()
                .Select(user => user.ToDomain())
                .ToListAsync(cancellationToken);

                return ApiResponse<IList<User>>.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while retrieving Users");
                return ApiResponse<IList<User>>.InternalError("An unexpected error occurred.");
            }
        }

        public async Task<ApiResponse<CreateUserResponse>> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userDto = request.ToDto();

                await dbContext.Users.AddAsync(userDto, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);

                if (userDto.Id == 0)
                {
                    return ApiResponse<CreateUserResponse>.InternalError("Failed to create user");
                }


                return ApiResponse<CreateUserResponse>.Created(new CreateUserResponse
                {
                    Id = userDto.Id
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while creating User with Data: {UserRequest}", JsonConvert.SerializeObject(request));
                return ApiResponse<CreateUserResponse>.InternalError("An unexpected error occurred.");
            }

        }

        public async Task<ApiResponse<string>> UpdateUserRolesAsync(int userId, UpdateUserRolesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userDto = await dbContext.Users
                .Include(user => user.Roles)
                .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);

                if (userDto == null)
                {
                    return ApiResponse<string>.NotFound($"User with ID: {userId} not found");
                }

                if (request.RoleIds.Count == 0)
                {
                    userDto.Roles.Clear();
                    await dbContext.SaveChangesAsync(cancellationToken);
                    return ApiResponse<string>.Ok($"Roles for User with ID: {userId} have been cleared");
                }

                var roleDtos = await dbContext.Roles
                    .Where(r => request.RoleIds.Contains(r.Id))
                    .ToListAsync(cancellationToken);

                var rolesNotFound = request.RoleIds.Except(roleDtos.Select(r => r.Id)).ToList();

                if (rolesNotFound.Count > 0)
                {
                    return ApiResponse<string>.NotFound($"Roles with IDs: {string.Join(", ", rolesNotFound)} not found");
                }

                var existingRoleIds = userDto.Roles.Select(r => r.Id).ToHashSet();
                var newRoleIds = request.RoleIds.ToHashSet();

                var rolesToAdd = roleDtos.Where(r => !existingRoleIds.Contains(r.Id));
                foreach (var role in rolesToAdd)
                {
                    userDto.Roles.Add(role);
                }

                var rolesToRemove = userDto.Roles.Where(r => !newRoleIds.Contains(r.Id)).ToList();
                foreach (var role in rolesToRemove)
                {
                    userDto.Roles.Remove(role);
                }
                await dbContext.SaveChangesAsync(cancellationToken);

                return ApiResponse<string>.Ok($"Roles for User with ID: {userId} have been updated");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while updating User Roles for User ID: {UserId}", userId);
                return ApiResponse<string>.InternalError("An unexpected error occurred.");
            }

        }
    }
}
