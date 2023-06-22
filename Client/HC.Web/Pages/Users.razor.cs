using HC.Shared.Constants;
using HC.Shared.Dtos.User;
using HC.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace HC.Web.Pages;

public partial class Users
{
    [Inject] protected IApiCaller _apiCaller { get; set; } = default!;

    UserResponseDto _user = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        var response = await _apiCaller.GetAsync<UserResponseDto>($"{ApiRoutingConstants.Auth.ControllerName}/{ApiRoutingConstants.Auth.GetById}?id={26}");
        _user = response.Data;
    }
}
