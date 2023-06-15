using HC.Common.Models;
using HC.Domain.Contracts;
using HC.Shared.Dtos;
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

    [HttpPost]
    [AllowAnonymous]
    public virtual async Task<ServerSideApiResult<TokenResponseDto>> GetToken(TokenRequestDto tokenRequest, CancellationToken cancellationToken)
    {
        return await _userRepository.GetToken(tokenRequest, cancellationToken);
    }

    [HttpGet]
    public virtual async Task<ServerSideApiResult<List<UserResponseDto>>> Get(CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpGet("{id:int}")]
    public virtual async Task<ServerSideApiResult<UserResponseDto>> Get(int id, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpPost]
    public virtual async Task<ServerSideApiResult<UserResponseDto>> Create(UserRequestDto dto, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpPut]
    public virtual async Task<ServerSideApiResult<UserResponseDto>> Update(int id, UserRequestDto dto, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public virtual async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        return Ok();
    }
}
