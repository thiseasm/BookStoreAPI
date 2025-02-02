using BookStore.Core.Abstractions.Interfaces.Services;
using BookStore.Core.Abstractions.Models.ApiResponses;
using BookStore.Core.Abstractions.Models.AuditLogs;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Api.Controllers
{
    [ApiController]
    [Route("api/audit/users")]
    public class UserLogsController(IUserLogService userLogService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IList<UserLog>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserLogsAsync(CancellationToken cancellationToken)
        {
            var result = await userLogService.GetUserLogsAsync(cancellationToken);
            return Ok(result.Data);
        }
    }
}
