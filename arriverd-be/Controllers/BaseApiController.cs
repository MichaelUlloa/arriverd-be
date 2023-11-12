using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace arriverd_be.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
}
