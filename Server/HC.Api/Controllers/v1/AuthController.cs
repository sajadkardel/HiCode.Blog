using HC.Common.Models;
using HC.Domain.Contracts;
using HC.Shared.Constants;
using HC.Shared.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HC.Api.Controllers.v1;

[ApiVersion("1")]
public class AuthController : BaseController
{
    private readonly IAuthRepository _authRepository;

    public AuthController(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    [HttpPost(RoutingConstants.ServerSide.Auth.SignUp)]
    [AllowAnonymous]
    public virtual async Task<ServerSideApiResult> SignUp([FromBody] SignUpRequestDto request, CancellationToken cancellationToken)
    {
        await _authRepository.SignUp(request, cancellationToken);
        return Ok();
    }

    [HttpPost(RoutingConstants.ServerSide.Auth.SignIn)]
    [AllowAnonymous]
    public virtual async Task<SignInResponseDto> SignIn([FromBody] SignInRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _authRepository.SignIn(request, cancellationToken);
        return result;
    }
}
