using HC.Common.Exceptions;
using HC.Common.Models;
using HC.Domain.Contracts;
using HC.Domain.Implementations;
using HC.Entity.Identity;
using HC.Service.Contracts;
using HC.Shared.Dtos.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HC.Api.Controllers.v1;

[ApiVersion("1")]
public class UsersController : BaseController
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost]
    [AllowAnonymous]
    public virtual async Task<ApiResult<LoginResponseDto>> Login([FromForm] LoginRequestDto tokenRequest, CancellationToken cancellationToken)
    {
        return await _userRepository.Login(tokenRequest, cancellationToken);
    }
}
