
using HC.Shared.Dtos.User;
using HC.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace HC.Web.Pages;

public partial class SignUp
{
    [Inject] protected IUserService _userService { get; set; } = default!;
    
    private string? _message = null;
    private SignUpRequestDto _signUpRequestDto = new();

    private async Task DoSignUp()
    {
        var result = await _userService.SignUp(_signUpRequestDto);
        _message = result.Message;
    }
}
