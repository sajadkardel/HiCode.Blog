using HC.Shared.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HC.Api.Controllers.v2;

[ApiVersion("1.1")]
public class UserController : v1.UserController
{
    public UserController(IUserService userService) : base(userService)
    {
    }
}
