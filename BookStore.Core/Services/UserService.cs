using Azure.Core;
using BookStore.Core.Abstractions.Events;
using BookStore.Core.Abstractions.Interfaces.Services;
using BookStore.Core.Abstractions.Models.ApiResponses;
using BookStore.Core.Abstractions.Models.Users;
using BookStore.Core.Extensions;
using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookStore.Core.Services
{
    public class UserService(ILogger<UserService> logger, IMediator mediator,BookStoreContext dbContext) : IUserService
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
                logger.LogError(ex, "Unexpected error while retrieving User with ID: {UserId}", userId);
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

        public async Task<ApiResponse<int>> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userDto = new UserDto
                {
                    Name = request.Name,
                    Surname = request.Surname
                };

                await dbContext.Users.AddAsync(userDto, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);

                if (userDto.Id == 0)
                {
                    return ApiResponse<int>.InternalError("Failed to create user");
                }

                var userCreatedEvent = new UserCreatedEvent(userDto.ToDomain(), DateTime.UtcNow);
                await mediator.Send(userCreatedEvent, cancellationToken);

                return ApiResponse<int>.Created(userDto.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while creating User with Data: {UserRequest}", JsonConvert.SerializeObject(request));
                return ApiResponse<int>.InternalError("An unexpected error occurred.");
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
                    return await ClearUserRolesAsync(userDto, cancellationToken);
                }

                var roleDtos = await dbContext.Roles
                    .Where(r => request.RoleIds.Contains(r.Id))
                    .ToListAsync(cancellationToken);

                var rolesNotFound = request.RoleIds.Except(roleDtos.Select(r => r.Id)).ToList();

                if (rolesNotFound.Count > 0)
                {
                    return ApiResponse<string>.NotFound($"Roles with IDs: {string.Join(", ", rolesNotFound)} not found");
                }

                return await UpdateUserRolesAsync(userDto, roleDtos, cancellationToken);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while updating User Roles for User ID: {UserId}", userId);
                return ApiResponse<string>.InternalError("An unexpected error occurred.");
            }
        }

        private async Task<ApiResponse<string>> ClearUserRolesAsync(UserDto userDto, CancellationToken cancellationToken)
        {
            if(userDto.Roles.Count == 0)
            {
                return ApiResponse<string>.Ok($"Roles for User with ID: {userDto.Id} are already up to date");
            }

            var rolesToClear = userDto.Roles.Select(r => r.Id).ToHashSet();
            userDto.Roles.Clear();
            await dbContext.SaveChangesAsync(cancellationToken);

            var rolesUpdatedEvent = new UserRolesUpdatedEvent(userDto.Id, rolesToClear, [], DateTime.UtcNow);
            await mediator.Send(rolesUpdatedEvent, cancellationToken);
            return ApiResponse<string>.Ok($"Roles for User with ID: {userDto.Id} have been cleared");
        }

        private async Task<ApiResponse<string>> UpdateUserRolesAsync(UserDto userDto, List<RoleDto> rolesToAdd, CancellationToken cancellationToken)
        {
            var existingRoleIds = userDto.Roles.Select(r => r.Id).ToHashSet();
            var newRoleIds = rolesToAdd.Select(r => r.Id).ToHashSet();

            if (existingRoleIds.SetEquals(newRoleIds))
            {
                return ApiResponse<string>.Ok($"Roles for User with ID: {userDto.Id} are already up to date");
            }

            userDto.Roles.Clear();
            userDto.Roles.AddRange(rolesToAdd);
            await dbContext.SaveChangesAsync(cancellationToken);

            var userRolesUpdatedEvent = new UserRolesUpdatedEvent(userDto.Id, existingRoleIds, newRoleIds, DateTime.UtcNow);
            await mediator.Send(userRolesUpdatedEvent, cancellationToken);

            return ApiResponse<string>.Ok($"Roles for User with ID: {userDto.Id} have been updated");
        }
    }
}
