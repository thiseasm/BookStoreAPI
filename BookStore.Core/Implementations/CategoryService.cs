using BookStore.Core.Abstractions.Interfaces;
using BookStore.Core.Abstractions.Models;
using BookStore.Core.Extensions;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Core.Implementations
{
    public class CategoryService(BookStoreContext dbContext) : ICategoryService
    {
        public async Task<IList<Category>> GetCategoriesAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Categories
                .Where(category => category.IsActive)
                .AsNoTracking()
                .Select(category => category.ToDomain())
                .ToListAsync(cancellationToken);
        }
    }
}
