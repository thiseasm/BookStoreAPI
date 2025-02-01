using BookStore.Core.Abstractions.Interfaces;
using BookStore.Core.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Api.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RolesController(IRoleService roleService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IList<Role>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRolesAsync(CancellationToken cancellationToken)
        {
            var result = await roleService.GetRolesAsync(cancellationToken);
            return Ok(result);
        }
    }
}
