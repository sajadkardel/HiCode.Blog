using Azure.Core;
using HC.DataAccess.Entities.User;
using HC.Service.Contracts;
using HC.Shared.Dtos.User;

namespace HC.Domain.Contracts;

public interface IUserRepository
{
    Task SignUp(SignUpRequestDto request, CancellationToken cancellationToken);
    Task<SignInResponseDto> SignIn(SignInRequestDto request, CancellationToken cancellationToken);
    Task<List<UserResponseDto>> GetAllUser(CancellationToken cancellationToken);
    Task<UserResponseDto> GetUserById(CancellationToken cancellationToken, int id);
}