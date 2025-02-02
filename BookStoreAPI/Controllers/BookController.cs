using BookStore.Core.Abstractions.Interfaces.Services;
using BookStore.Core.Abstractions.Models.ApiResponses;
using BookStore.Core.Abstractions.Models.Books;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Api.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController(IBookService bookService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IList<Book>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>),StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBooksAsync(CancellationToken cancellationToken)
        {
            var result = await bookService.GetBooksAsync(cancellationToken);
            return result.Success
                ? Ok(result.Data)
                : StatusCode(result.Code, result.Error);
        }

        [HttpGet("{id:int:min(1)}")]
        [ProducesResponseType(typeof(ApiResponse<Book>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBookByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await bookService.GetBookByIdAsync(id, cancellationToken);
            return result.Success
                ? Ok(result.Data)
                : StatusCode(result.Code, result.Error);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookRequest request, CancellationToken cancellationToken)
        {
            var result = await bookService.CreateBookAsync(request, cancellationToken);
            return result.Success
                ? Ok(result.Data)
                : StatusCode(result.Code, result.Error);
        }
    }
}
