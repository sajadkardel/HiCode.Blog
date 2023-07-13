using HC.Shared.Dtos.Auth;
using HC.Shared.Models;

namespace HC.Web.Services.Contracts;

public interface IAuthService
{
    public Task<ApiResult<SignUpResponseDto>> SignUp(SignUpRequestDto request);
    public Task<ApiResult<SignInResponseDto>> SignIn(SignInRequestDto request);
    public Task SignOut();
}
