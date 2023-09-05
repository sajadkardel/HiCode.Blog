using HC.Shared.Dtos.Auth;
using HC.Shared.Dtos.User;
using HC.Web.Services.Contracts;
using HC.Web.Shared;
using Microsoft.AspNetCore.Components;

namespace HC.Web.Pages.User;

public partial class SignIn : AppBaseComponent
{
    [Inject] protected IAuthService _authService { get; set; } = default!;

    private string? _message = null;
    private SignInRequestDto _signInModel = new();

	private async Task DoSignIn()
	{
        var result = await _authService.SignIn(_signInModel);
        _message = result.Message;
    }
}
