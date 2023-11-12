using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace arriverd_be.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected BadRequestObjectResult BadRequest(string detail)
    {
        var problem = new ProblemDetails()
        {
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1",
            Status = (int?)HttpStatusCode.BadRequest,
            Title = "Bad Request",
            Detail = detail,
            Extensions =
            {
                { "traceId", Activity.Current?.Id ?? HttpContext.TraceIdentifier }
            }
        };

        return BadRequest(problem);
    }
}
