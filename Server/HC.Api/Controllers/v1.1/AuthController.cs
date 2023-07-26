using HC.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HC.Api.Controllers.v2;

[ApiVersion("1.1")]
public class AuthController : v1.AuthController
{
    public AuthController(IAuthService authRepository) : base(authRepository)
    {
    }
}
