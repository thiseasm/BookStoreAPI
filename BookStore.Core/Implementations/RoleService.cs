using BookStore.Core.Abstractions.Interfaces;
using BookStore.Core.Abstractions.Models;
using BookStore.Core.Abstractions.Models.ApiResponses;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Core.Implementations
{
    public class RoleService(BookStoreContext dbContext) : IRoleService
    {
        public async Task<ApiResponse<IList<Role>>> GetRolesAsync(CancellationToken cancellationToken)
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
    }
}
