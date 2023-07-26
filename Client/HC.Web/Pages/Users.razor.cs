using HC.Shared.Dtos.User;
using HC.Shared.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace HC.Web.Pages;

public partial class Users
{
    [Inject] protected IUserService _userService { get; set; } = default!;

    List<UserResponseDto> _users = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        var response = await _userService.GetAll();
        _users = response.Data;
    }
}
