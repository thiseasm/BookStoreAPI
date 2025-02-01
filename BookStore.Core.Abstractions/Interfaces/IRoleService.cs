using BookStore.Core.Abstractions.Models;

namespace BookStore.Core.Abstractions.Interfaces
{
    public interface IRoleService
    {
        Task<IList<Role>> GetRolesAsync(CancellationToken cancellationToken = default);
    }
}
