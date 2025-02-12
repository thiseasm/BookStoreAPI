﻿using BookStore.Core.Abstractions.Models.ApiResponses;
using BookStore.Core.Abstractions.Models.Roles;

namespace BookStore.Core.Abstractions.Interfaces.Services
{
    public interface IRoleService
    {
        Task<ApiResponse<IList<Role>>> GetRolesAsync(CancellationToken cancellationToken = default);
    }
}
