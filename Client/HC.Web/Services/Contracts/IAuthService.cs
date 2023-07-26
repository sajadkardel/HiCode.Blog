using HC.Shared.Dtos.Auth;
using HC.Shared.Models;

namespace HC.Web.Services.Contracts;

public interface IAuthService
{
    public Task<Result<SignUpResponseDto>> SignUp(SignUpRequestDto request, CancellationToken cancellationToken = default);
    public Task<Result<SignInResponseDto>> SignIn(SignInRequestDto request, CancellationToken cancellationToken = default);
    public Task SignOut(CancellationToken cancellationToken = default);
}
