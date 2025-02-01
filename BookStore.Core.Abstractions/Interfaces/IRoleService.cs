using BookStore.Core.Abstractions.Models;
using BookStore.Core.Abstractions.Models.ApiResponses;

namespace BookStore.Core.Abstractions.Interfaces
{
    public interface IRoleService
    {
        Task<ApiResponse<IList<Role>>> GetRolesAsync(CancellationToken cancellationToken = default);
    }
}
