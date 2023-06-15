using Azure.Core;
using HC.Entity.Identity;
using HC.Shared.Dtos.Identity;

namespace HC.Domain.Contracts;

public interface IUserRepository
{
    Task<LoginResponseDto> GetToken(LoginRequestDto request, CancellationToken cancellationToken);
    Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken);
}