
using HC.Shared.Dtos.User;
using HC.Web.Services.Contracts;

namespace HC.Web.Pages;

public partial class SignIn
{
    private readonly IUserService _userService;
	public SignIn(IUserService userService)
	{
		_userService = userService;
	}

	private TokenRequestDto _signInModel = new();

	private async Task DoSignIn()
	{
        await _userService.SignIn(_signInModel);
    }
}
