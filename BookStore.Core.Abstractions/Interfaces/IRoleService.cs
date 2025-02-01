using BookStore.Core.Abstractions.Models.Roles;

namespace BookStore.Core.Abstractions.Interfaces
{
    public interface IRoleService
    {
        Task<IList<RoleResponse>> GetRolesAsync(CancellationToken cancellationToken = default);
    }
}
