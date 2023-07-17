using HC.Service.Contracts;
using HC.Shared.Constants;
using HC.Shared.Dtos.User;
using HC.Shared.Models;
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
    public virtual async Task<ApiResult<List<UserResponseDto>>> GetAll(CancellationToken cancellationToken = default)
    {
        var result = await _userService.GetAll(cancellationToken);
        return ApiResult.Success(result);
    }

    [HttpGet(RoutingConstants.ServerSide.User.GetById)]
    public virtual async Task<ApiResult<UserResponseDto>> GetById([FromQuery] int id, CancellationToken cancellationToken = default)
    {
        var result = await _userService.GetById(id, cancellationToken);
        return ApiResult.Success(result);
    }

    [HttpPost(RoutingConstants.ServerSide.User.Create)]
    public virtual async Task<ApiResult> Create([FromBody] UserRequestDto dto, CancellationToken cancellationToken = default)
    {
        return ApiResult.Success();
    }

    [HttpPut(RoutingConstants.ServerSide.User.Update)]
    public virtual async Task<ApiResult> Update([FromQuery] int id, [FromBody] UserRequestDto dto, CancellationToken cancellationToken = default)
    {
        return ApiResult.Success();
    }

    [HttpDelete(RoutingConstants.ServerSide.User.Delete)]
    public virtual async Task<ApiResult> Delete([FromQuery] int id, CancellationToken cancellationToken = default)
    {
        return ApiResult.Success();
    }
}
