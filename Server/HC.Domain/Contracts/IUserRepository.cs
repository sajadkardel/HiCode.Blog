using HC.Shared.Dtos.User;

namespace HC.Domain.Contracts;

public interface IUserRepository
{
    Task<List<UserResponseDto>> GetAllUser(CancellationToken cancellationToken);
    Task<UserResponseDto> GetUserById(CancellationToken cancellationToken, int id);
}