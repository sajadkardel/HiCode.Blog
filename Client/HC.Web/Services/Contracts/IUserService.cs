using HC.Shared.Dtos.User;
using HC.Shared.Models;

namespace HC.Web.Services.Contracts;

public interface IUserService
{
    public Task<Result<List<UserResponseDto>>> GetAll();
    public Task<Result<UserResponseDto>> GetById(int id);
}
