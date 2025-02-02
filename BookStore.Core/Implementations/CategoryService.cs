using BookStore.Core.Abstractions.Interfaces;
using BookStore.Core.Abstractions.Models;
using BookStore.Core.Abstractions.Models.ApiResponses;
using BookStore.Core.Extensions;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookStore.Core.Implementations
{
    public class CategoryService(ILogger<CategoryService> logger, BookStoreContext dbContext) : ICategoryService
    {
        public async Task<ApiResponse<string>> DeleteCategoryAsync(int categoryId, CancellationToken cancellationToken)
        {
            try
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
                    return ApiResponse<string>.Conflict($"Category with ID: {categoryId} is in use by {booksThatUseCategory} Books");
                };

                category.IsActive = false;
                await dbContext.SaveChangesAsync(cancellationToken);

                return ApiResponse<string>.Ok("Category deleted successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while deleting Category with ID: {category}", categoryId);
                return ApiResponse<string>.InternalError("An unexpected error occurred.");
            }
        }

        public async Task<ApiResponse<IList<Category>>> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var result = await dbContext.Categories
                .Where(category => category.IsActive)
                .AsNoTracking()
                .Select(category => category.ToDomain())
                .ToListAsync(cancellationToken);

                return ApiResponse<IList<Category>>.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while retrieving Categories");
                return ApiResponse<IList<Category>>.InternalError("An unexpected error occurred.");
            }

        }
    }
}
