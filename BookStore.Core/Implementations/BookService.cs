using BookStore.Core.Abstractions.Interfaces;
using BookStore.Core.Abstractions.Models;
using BookStore.Core.Abstractions.Models.ApiResponses;
using BookStore.Core.Extensions;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Core.Implementations
{
    public class BookService(BookStoreContext dbContext) : IBookService
    {
        public async Task<ApiResponse<IList<Book>>> GetBooksAsync(CancellationToken cancellationToken)
        {
            var result = await dbContext.Books
                .Include(book => book.Category)
                .AsNoTracking()
                .Select(book => book.ToDomain())
                .ToListAsync(cancellationToken);

            return ApiResponse<IList<Book>>.Ok(result);
        }
    }
}
