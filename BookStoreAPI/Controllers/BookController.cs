using BookStore.Core.Abstractions.Interfaces;
using BookStore.Core.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Api.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController(IBookService bookService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IList<Book>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBooksAsync(CancellationToken cancellationToken)
        {
            var result = await bookService.GetBooksAsync(cancellationToken);
            return Ok(result);
        }
    }
}
