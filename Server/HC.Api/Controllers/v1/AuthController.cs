using HC.Common.Models;
using HC.Domain.Contracts;
using HC.Shared.Constants;
using HC.Shared.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HC.Api.Controllers.v1;

[ApiVersion("1")]
public class AuthController : BaseController
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost(RoutingConstants.ServerSide.Auth.SignUp)]
    [AllowAnonymous]
    public virtual async Task<ServerSideApiResult> SignUp([FromBody] SignUpRequestDto request, CancellationToken cancellationToken)
    {
        await _userRepository.SignUp(request, cancellationToken);
        return Ok();
    }

    [HttpPost(RoutingConstants.ServerSide.Auth.SignIn)]
    [AllowAnonymous]
    public virtual async Task<SignInResponseDto> SignIn([FromBody] SignInRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.SignIn(request, cancellationToken);
        return result;
    }

    [HttpGet(RoutingConstants.ServerSide.Auth.GetAll)]
    public virtual async Task<List<UserResponseDto>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetAllUser(cancellationToken);
        return result;
    }

    [HttpGet(RoutingConstants.ServerSide.Auth.GetById)]
    public virtual async Task<UserResponseDto> GetById([FromQuery]int id, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetUserById(cancellationToken, id);
        return result;
    }

    [HttpPost(RoutingConstants.ServerSide.Auth.Create)]
    public virtual async Task<ServerSideApiResult<UserResponseDto>> Create([FromBody] UserRequestDto dto, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpPut(RoutingConstants.ServerSide.Auth.Update)]
    public virtual async Task<ServerSideApiResult<UserResponseDto>> Update([FromQuery] int id, [FromBody] UserRequestDto dto, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpDelete(RoutingConstants.ServerSide.Auth.Delete)]
    public virtual async Task<ServerSideApiResult> Delete([FromQuery]int id, CancellationToken cancellationToken)
    {
        return Ok();
    }
}
