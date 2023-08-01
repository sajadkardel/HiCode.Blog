using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HC.Api.Controllers;

[ApiController]
//[Authorize]
[AllowAnonymous]
[Route("api/v{version:apiVersion}/")]
public class BaseController : ControllerBase
{

}
