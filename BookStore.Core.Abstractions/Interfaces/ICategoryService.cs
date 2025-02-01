using BookStore.Core.Abstractions.Models;
using BookStore.Core.Abstractions.Models.ApiResponses;

namespace BookStore.Core.Abstractions.Interfaces
{
    public interface ICategoryService
    {
        public Task<ApiResponse<IList<Category>>> GetCategoriesAsync(CancellationToken cancellationToken = default);
        public Task<ApiResponse<string>> DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
    }
}
