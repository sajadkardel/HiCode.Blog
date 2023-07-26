using HC.Shared.Constants;
using HC.Shared.Dtos.User;
using HC.Shared.Models;
using HC.Shared.Services.Contracts;
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
    public virtual async Task<Result<List<UserResponseDto>>> GetAll(CancellationToken cancellationToken = default)
    {
        var result = await _userService.GetAll(cancellationToken);
        return Result.Success(result.Data);
    }

    [HttpGet(RoutingConstants.ServerSide.User.GetById)]
    public virtual async Task<Result<UserResponseDto>> GetById([FromQuery] int id, CancellationToken cancellationToken = default)
    {
        var result = await _userService.GetById(id, cancellationToken);
        return Result.Success(result.Data);
    }

    [HttpPost(RoutingConstants.ServerSide.User.Create)]
    public virtual async Task<Result> Create([FromBody] UserRequestDto dto, CancellationToken cancellationToken = default)
    {
        return Result.Success();
    }

    [HttpPut(RoutingConstants.ServerSide.User.Update)]
    public virtual async Task<Result> Update([FromQuery] int id, [FromBody] UserRequestDto dto, CancellationToken cancellationToken = default)
    {
        return Result.Success();
    }

    [HttpDelete(RoutingConstants.ServerSide.User.Delete)]
    public virtual async Task<Result> Delete([FromQuery] int id, CancellationToken cancellationToken = default)
    {
        return Result.Success();
    }
}
