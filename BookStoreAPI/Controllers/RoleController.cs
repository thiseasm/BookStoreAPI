using BookStore.Core.Abstractions.Interfaces;
using BookStore.Core.Abstractions.Models;
using BookStore.Core.Abstractions.Models.ApiResponses;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Api.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RolesController(IRoleService roleService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IList<Role>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRolesAsync(CancellationToken cancellationToken)
        {
            var result = await roleService.GetRolesAsync(cancellationToken);
            return Ok(result.Data);
        }
    }
}
