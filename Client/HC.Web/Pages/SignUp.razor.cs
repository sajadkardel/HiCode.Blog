
using HC.Shared.Dtos.User;
using HC.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace HC.Web.Pages;

public partial class SignUp
{
    [Inject] protected IUserService _userService { get; set; } = default!;

    private SignUpRequestDto _signUpRequestDto = new();

    private async Task DoSignUp()
    {
        await _userService.SignUp(_signUpRequestDto);
    }
}
