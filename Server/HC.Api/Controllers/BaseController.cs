using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HC.WebFramework.ActionFilters;

namespace HC.Api.Controllers;

[ApiController]
[Authorize]
[ApiResultFilter]
[Route("api/v{version:apiVersion}/")]
public class BaseController : ControllerBase
{

}
