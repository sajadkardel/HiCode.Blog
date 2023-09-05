using HC.Shared.Dtos.Auth;
using HC.Web.Services.Contracts;
using HC.Web.Shared;
using Microsoft.AspNetCore.Components;

namespace HC.Web.Pages.User;

public partial class SignUp : AppBaseComponent
{
    [Inject] protected IAuthService _authService { get; set; } = default!;

    private string? _message = null;
    private SignUpRequestDto _signUpRequestDto = new();

    private async Task DoSignUp()
    {
        var result = await _authService.SignUp(_signUpRequestDto);
        _message = result.Message;
    }
}
