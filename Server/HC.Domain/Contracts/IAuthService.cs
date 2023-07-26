using HC.Shared.Dtos.Auth;
using HC.Shared.Models;

namespace HC.Domain.Contracts;

public interface IAuthService
{
    Task<Result> SignUp(SignUpRequestDto request, CancellationToken cancellationToken = default);
    Task<Result<SignInResponseDto>> SignIn(SignInRequestDto request, CancellationToken cancellationToken = default);
}
