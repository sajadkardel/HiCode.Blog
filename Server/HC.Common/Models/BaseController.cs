using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HC.Common.Attributes;

namespace HC.Common.Models;

[ApiController]
[Authorize]
[ApiResultFilter]
//[Route("api/v{version:apiVersion}")]
public class BaseController : ControllerBase
{
    //public UserRepository UserRepository { get; set; } => property injection
    //public bool UserIsAuthenticated => HttpContext.User.Identity is { IsAuthenticated: true };
}
