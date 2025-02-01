using BookStore.Core.Abstractions.Interfaces;
using BookStore.Core.Abstractions.Models;
using BookStore.Core.Abstractions.Models.ApiResponses;
using BookStore.Core.Extensions;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Core.Implementations
{
    public class CategoryService(BookStoreContext dbContext) : ICategoryService
    {
        public async Task<ApiResponse<string>> DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken)
        {
            var category = await dbContext.Categories
                .Where(category => category.Id == categoryId && category.IsActive)
                .FirstOrDefaultAsync(cancellationToken);

            if (category is null)
            {
                return ApiResponse<string>.NotFound($"Category with ID: {categoryId} not found");
            }

            var booksThatUseCategory = await dbContext.Books
                .Where(book => book.CategoryId == categoryId)
                .AsNoTracking()
                .CountAsync(cancellationToken);

            if (booksThatUseCategory > 0)
            {
                return ApiResponse<string>.Conflict($"Category is in use by {booksThatUseCategory} books");
            };

            category.IsActive = false;
            await dbContext.SaveChangesAsync(cancellationToken);

            return ApiResponse<string>.Ok("Category deleted successfully");

        }

        public async Task<ApiResponse<IList<Category>>> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            var result = await dbContext.Categories
                .Where(category => category.IsActive)
                .AsNoTracking()
                .Select(category => category.ToDomain())
                .ToListAsync(cancellationToken);

            return ApiResponse<IList<Category>>.Ok(result);
        }
    }
}
