using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HC.Common.Attributes;

namespace HC.Common.Models;

[ApiController]
[Authorize]
[ApiResultFilter]
[Route("api/v{version:apiVersion}/")]
public class BaseController : ControllerBase
{

}
