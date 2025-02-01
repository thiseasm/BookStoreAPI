using BookStore.Core.Abstractions.Models;
using BookStore.Core.Abstractions.Models.ApiResponses;

namespace BookStore.Core.Abstractions.Interfaces
{
    public interface IBookService
    {
        public Task<ApiResponse<IList<Book>>> GetBooksAsync(CancellationToken cancellationToken = default);
    }
}
