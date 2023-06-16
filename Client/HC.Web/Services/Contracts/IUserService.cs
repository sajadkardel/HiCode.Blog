using HC.Shared.Dtos.User;
using HC.Web.Models;

namespace HC.Web.Services.Contracts;

public interface IUserService
{
    public Task<ClientSideApiResult> SignUp(SignUpRequestDto request);
    public Task<ClientSideApiResult<SignInResponseDto>> SignIn(SignInRequestDto request);
    public Task SignOut();
}
