using HC.Shared.Dtos.User;

namespace HC.Service.Contracts;

public interface IUserService
{
    Task<List<UserResponseDto>> GetAll(CancellationToken cancellationToken);
    Task<UserResponseDto> GetById(CancellationToken cancellationToken, int id);
}
