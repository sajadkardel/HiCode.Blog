using HC.Service.Contracts;
using HC.Shared.Constants;
using HC.Shared.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace HC.Api.Controllers.v1;

[ApiVersion("1.0")]
public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet(RoutingConstants.ServerSide.User.GetAll)]
    public virtual async Task<List<UserResponseDto>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _userService.GetAll(cancellationToken);
        return result;
    }

    [HttpGet(RoutingConstants.ServerSide.User.GetById)]
    public virtual async Task<UserResponseDto> GetById([FromQuery] int id, CancellationToken cancellationToken)
    {
        var result = await _userService.GetById(cancellationToken, id);
        return result;
    }

    [HttpPost(RoutingConstants.ServerSide.User.Create)]
    public virtual async Task<IActionResult> Create([FromBody] UserRequestDto dto, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpPut(RoutingConstants.ServerSide.User.Update)]
    public virtual async Task<IActionResult> Update([FromQuery] int id, [FromBody] UserRequestDto dto, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpDelete(RoutingConstants.ServerSide.User.Delete)]
    public virtual async Task<IActionResult> Delete([FromQuery] int id, CancellationToken cancellationToken)
    {
        return Ok();
    }
}
