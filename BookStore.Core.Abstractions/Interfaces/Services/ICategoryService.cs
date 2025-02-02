using BookStore.Core.Abstractions.Models.ApiResponses;
using BookStore.Core.Abstractions.Models.Categories;

namespace BookStore.Core.Abstractions.Interfaces.Services
{
    public interface ICategoryService
    {
        public Task<ApiResponse<IList<Category>>> GetCategoriesAsync(CancellationToken cancellationToken = default);
        public Task<ApiResponse<string>> DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken = default);
    }
}
