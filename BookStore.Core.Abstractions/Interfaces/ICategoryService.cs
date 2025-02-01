using BookStore.Core.Abstractions.Models;

namespace BookStore.Core.Abstractions.Interfaces
{
    public interface ICategoryService
    {
        public Task<IList<Category>> GetCategoriesAsync(CancellationToken cancellationToken = default);
    }
}
