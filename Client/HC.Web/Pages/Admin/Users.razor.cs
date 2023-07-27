using HC.Shared.Dtos.User;
using HC.Shared.Services.Contracts;
using HC.Web.Shared;
using Microsoft.AspNetCore.Components;

namespace HC.Web.Pages.Admin;

public partial class Users : AppBaseComponent
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
