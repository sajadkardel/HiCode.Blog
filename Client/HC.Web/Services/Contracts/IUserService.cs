using HC.Shared.Dtos.User;
using HC.Shared.Models;

namespace HC.Web.Services.Contracts;

public interface IUserService
{
    public Task<ApiResult<List<UserResponseDto>>> GetAll();
    public Task<ApiResult<UserResponseDto>> GetById(int id);
}
