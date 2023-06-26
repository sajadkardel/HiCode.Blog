using HC.Shared.Dtos.User;
using HC.Web.Models;

namespace HC.Web.Services.Contracts;

public interface IUserService
{
    public Task<ClientSideApiResult<List<UserResponseDto>>> GetAll();
    public Task<ClientSideApiResult<UserResponseDto>> GetById(int id);
}
