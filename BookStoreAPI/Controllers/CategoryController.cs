using BookStore.Core.Abstractions.Interfaces;
using BookStore.Core.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Api.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IList<Category>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCategoriessAsync(CancellationToken cancellationToken)
        {
            var result = await categoryService.GetCategoriesAsync(cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCategoryAsync(int id, CancellationToken cancellationToken)
        {
            var result = await categoryService.DeleteCategoryAsync(id, cancellationToken);
            return result.Success 
                ? Ok(result.Data) 
                : StatusCode(result.Code, result.Error);
        }
    }
}
