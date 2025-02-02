using BookStore.Core.Abstractions.Interfaces.Services;
using BookStore.Core.Abstractions.Models.ApiResponses;
using BookStore.Core.Abstractions.Models.Books;
using BookStore.Core.Extensions;
using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BookStore.Core.Services
{
    public class BookService(ILogger<BookService> logger, BookStoreContext dbContext) : IBookService
    {
        public async Task<ApiResponse<Book>> GetBookByIdAsync(int bookId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await dbContext.Books
                    .Include(book => book.Category)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(book => book.Id == bookId, cancellationToken);

                return result != null
                    ? ApiResponse<Book>.Ok(result.ToDomain())
                    : ApiResponse<Book>.NotFound($"Book with ID: {bookId} not found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while retrieving Book with ID: {BookId}", bookId);
                return ApiResponse<Book>.InternalError("An unexpected error occurred.");
            }
        }

        public async Task<ApiResponse<IList<Book>>> GetBooksAsync(CancellationToken cancellationToken)
        {
            try
            {
                var result = await dbContext.Books
                .Include(book => book.Category)
                .AsNoTracking()
                .Select(book => book.ToDomain())
                .ToListAsync(cancellationToken);

                return ApiResponse<IList<Book>>.Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while retrieving Books");
                return ApiResponse<IList<Book>>.InternalError("An unexpected error occurred.");
            }
        }

        public async Task<ApiResponse<int>> CreateBookAsync(CreateBookRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var categoryExists = await dbContext.Categories.AnyAsync(category => category.Id == request.CategoryId, cancellationToken);

                if (!categoryExists)
                {
                    return ApiResponse<int>.BadRequest($"Category with ID: {request.CategoryId} does not exist");
                }

                var bookDto = new BookDto { Name = request.Name, CategoryId = request.CategoryId };

                await dbContext.Books.AddAsync(bookDto, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);

                if (bookDto.Id == 0)
                {
                    return ApiResponse<int>.InternalError("Failed to create book");
                }


                return ApiResponse<int>.Created(bookDto.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unexpected error while creating Book with Data: {BookRequest}", JsonConvert.SerializeObject(request));
                return ApiResponse<int>.InternalError("An unexpected error occurred.");
            }
        }
    }
}
