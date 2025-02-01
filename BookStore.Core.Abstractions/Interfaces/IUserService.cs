﻿using BookStore.Core.Abstractions.Models;
using BookStore.Core.Abstractions.Models.ApiResponses;
using BookStore.Core.Abstractions.Models.Users;

namespace BookStore.Core.Abstractions.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<IList<User>>> GetUsersAsync(CancellationToken cancellationToken = default);
        Task<ApiResponse<CreateUserResponse>> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default);
        Task<ApiResponse<string>> UpdateUserRolesAsync(int id, UpdateUserRolesRequest request, CancellationToken cancellationToken = default);
    }
}
