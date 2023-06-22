using HC.Shared.Dtos.User;
using HC.Web.Models;

namespace HC.Web.Services.Contracts;

public interface IUserService
{
    public Task<ClientSideApiResult<SignUpResponseDto>> SignUp(SignUpRequestDto request);
    public Task<ClientSideApiResult<SignInResponseDto>> SignIn(SignInRequestDto request);
    public Task SignOut();
    public Task<ClientSideApiResult<List<UserResponseDto>>> GetAll();
    public Task<ClientSideApiResult<UserResponseDto>> GetById(int id);
}
