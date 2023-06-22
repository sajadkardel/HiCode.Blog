using HC.Shared.Constants;
using HC.Shared.Dtos.User;
using HC.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace HC.Web.Pages;

public partial class Users
{
    [Inject] protected IApiCaller _apiCaller { get; set; } = default!;

    List<UserResponseDto> _users = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        var response = await _apiCaller.GetAsync<List<UserResponseDto>>($"{ApiRoutingConstants.Auth.ControllerName}/{ApiRoutingConstants.Auth.Get}");
        _users = response.Data;
    }
}
