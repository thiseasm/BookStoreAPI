using BookStore.Core.Abstractions.Interfaces;
using BookStore.Core.Abstractions.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            var result = await userService.CreateUserAsync(request, cancellationToken);
            return new JsonResult(result) { StatusCode = StatusCodes.Status201Created };
        }
    }
}
