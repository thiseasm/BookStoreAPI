using BookStore.Core.Abstractions.Interfaces;
using BookStore.Core.Abstractions.Models;
using BookStore.Core.Abstractions.Models.ApiResponses;
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
    }
}
