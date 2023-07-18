using HC.Shared.Dtos.User;
using HC.Shared.Models;

namespace HC.Service.Contracts;

public interface IUserService
{
    Task<Result<List<UserResponseDto>>> GetAll(CancellationToken cancellationToken = default);
    Task<Result<UserResponseDto>> GetById(int id, CancellationToken cancellationToken = default);
}
