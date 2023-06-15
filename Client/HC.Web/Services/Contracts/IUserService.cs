using HC.Shared.Dtos.User;

namespace HC.Web.Services.Contracts;

public interface IUserService
{
    public Task SignIn(TokenRequestDto request);
    public Task SignOut();
}
