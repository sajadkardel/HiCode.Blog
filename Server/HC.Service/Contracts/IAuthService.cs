using HC.Shared.Dtos.Auth;

namespace HC.Service.Contracts;

public interface IAuthService
{
    Task SignUp(SignUpRequestDto request, CancellationToken cancellationToken);
    Task<SignInResponseDto> SignIn(SignInRequestDto request, CancellationToken cancellationToken);
}
