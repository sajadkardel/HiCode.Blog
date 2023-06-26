using HC.Common.Models;
using HC.Domain.Contracts;
using HC.Shared.Constants;
using HC.Shared.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace HC.Api.Controllers.v1;

[ApiVersion("1")]
public class UserController : BaseController
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet(RoutingConstants.ServerSide.User.GetAll)]
    public virtual async Task<List<UserResponseDto>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetAllUser(cancellationToken);
        return result;
    }

    [HttpGet(RoutingConstants.ServerSide.User.GetById)]
    public virtual async Task<UserResponseDto> GetById([FromQuery] int id, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetUserById(cancellationToken, id);
        return result;
    }

    [HttpPost(RoutingConstants.ServerSide.User.Create)]
    public virtual async Task<ServerSideApiResult<UserResponseDto>> Create([FromBody] UserRequestDto dto, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpPut(RoutingConstants.ServerSide.User.Update)]
    public virtual async Task<ServerSideApiResult<UserResponseDto>> Update([FromQuery] int id, [FromBody] UserRequestDto dto, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpDelete(RoutingConstants.ServerSide.User.Delete)]
    public virtual async Task<ServerSideApiResult> Delete([FromQuery] int id, CancellationToken cancellationToken)
    {
        return Ok();
    }
}
