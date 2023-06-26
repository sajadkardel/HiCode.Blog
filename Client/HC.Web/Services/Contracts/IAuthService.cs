using HC.Shared.Dtos.Auth;
using HC.Web.Models;

namespace HC.Web.Services.Contracts;

public interface IAuthService
{
    public Task<ClientSideApiResult<SignUpResponseDto>> SignUp(SignUpRequestDto request);
    public Task<ClientSideApiResult<SignInResponseDto>> SignIn(SignInRequestDto request);
    public Task SignOut();
}
