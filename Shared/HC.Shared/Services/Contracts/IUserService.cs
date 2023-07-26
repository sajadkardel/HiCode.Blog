using HC.Shared.Dtos.User;
using HC.Shared.Models;

namespace HC.Shared.Services.Contracts;

public interface IUserService
{
    public Task<Result<List<UserResponseDto>>> GetAll(CancellationToken cancellationToken = default);
    public Task<Result<UserResponseDto>> GetById(int id, CancellationToken cancellationToken = default);
}
