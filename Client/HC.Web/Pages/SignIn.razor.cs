using HC.Shared.Dtos.User;
using HC.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace HC.Web.Pages;

public partial class SignIn
{
    [Inject] protected IUserService _userService { get; set; } = default!;

    private string? _message = null;
    private SignInRequestDto _signInModel = new();

	private async Task DoSignIn()
	{
        var result = await _userService.SignIn(_signInModel);
        _message = result.Message;
    }
}
