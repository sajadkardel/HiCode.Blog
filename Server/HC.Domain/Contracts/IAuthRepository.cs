using HC.Shared.Dtos.Auth;

namespace HC.Domain.Contracts;

public interface IAuthRepository
{
    Task SignUp(SignUpRequestDto request, CancellationToken cancellationToken);
    Task<SignInResponseDto> SignIn(SignInRequestDto request, CancellationToken cancellationToken);
}
