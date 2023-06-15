using Azure.Core;
using HC.DataAccess.Entities.User;
using HC.Shared.Dtos.User;

namespace HC.Domain.Contracts;

public interface IUserRepository
{
    Task<TokenResponseDto> GetToken(TokenRequestDto request, CancellationToken cancellationToken);
    Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken);
}