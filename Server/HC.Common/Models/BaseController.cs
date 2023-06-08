using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HC.Common.Attributes;

namespace HC.Common.Models;

[ApiController]
[AllowAnonymous]
[ApiResultFilter]
[Route("api/v{version:apiVersion}/[controller]/[action]")]// api/v1/[controller]/[action]
public class BaseController : ControllerBase
{
    //public UserRepository UserRepository { get; set; } => property injection
    public bool UserIsAuthenticated => HttpContext.User.Identity is { IsAuthenticated: true };
}
