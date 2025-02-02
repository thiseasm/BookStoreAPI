using BookStore.Core.Abstractions.Interfaces;
using BookStore.Core.Abstractions.Models;
using BookStore.Core.Abstractions.Models.ApiResponses;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.Core.Services
{
    public class RoleService(ILogger<RoleService> logger, BookStoreContext dbContext) : IRoleService
    {
        public async Task<ApiResponse<IList<Role>>> GetRolesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var result = await dbContext.Roles
                    .OrderBy(role => role.Id)
                    .AsNoTracking()
                    .Select(role => new Role
                    {
                        Id = role.Id,
                        Name = role.Name
                    })
                    .ToListAsync(cancellationToken);

                return ApiResponse<IList<Role>>.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while retrieving Roles");
                return ApiResponse<IList<Role>>.InternalError("An unexpected error occurred.");
            }           
        }
    }
}
