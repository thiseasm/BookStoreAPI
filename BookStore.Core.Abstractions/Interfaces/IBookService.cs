using BookStore.Core.Abstractions.Models.ApiResponses;
using BookStore.Core.Abstractions.Models.Books;

namespace BookStore.Core.Abstractions.Interfaces
{
    public interface IBookService
    {
        Task<ApiResponse<Book>> GetBookByIdAsync(int bookId, CancellationToken cancellationToken = default);
        Task<ApiResponse<IList<Book>>> GetBooksAsync(CancellationToken cancellationToken = default);
        Task<ApiResponse<int>> CreateBookAsync(CreateBookRequest request, CancellationToken cancellationToken = default);
    }
}
