using BookStore.Core.Abstractions.Models;

namespace BookStore.Core.Abstractions.Interfaces
{
    public interface IBookService
    {
        public Task<IList<Book>> GetBooksAsync(CancellationToken cancellationToken = default);
    }
}
