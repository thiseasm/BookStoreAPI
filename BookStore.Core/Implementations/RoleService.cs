using BookStore.Core.Abstractions.Interfaces;
using BookStore.Core.Abstractions.Models.Roles;
using BookStore.Data.Abstractions.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Core.Implementations
{
    public class RoleService(BookStoreContext dbContext) : IRoleService
    {
        public async Task<IList<RoleResponse>> GetRolesAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Roles
                .OrderBy(role => role.Id)
                .AsNoTracking()
                .Select(role => new RoleResponse
                {
                    Id = role.Id,
                    Name = role.Name
                })
                .ToListAsync(cancellationToken);
        }
    }
}
