using HC.Shared.Dtos.User;
using HC.Shared.Markers;
using HC.Web.Services.Contracts;

namespace HC.Web.Services.Implementations;

public class UserService : IUserService, IScopedDependency
{
    private readonly IApiCaller _apiCaller;
    private readonly IStorageService _localStorageService;
    private readonly AppAuthenticationStateProvider _appAuthenticationStateProvider;

    public UserService(IApiCaller apiCaller, IStorageService localStorageService, AppAuthenticationStateProvider appAuthenticationStateProvider)
    {
        _apiCaller = apiCaller;
        _localStorageService = localStorageService;
        _appAuthenticationStateProvider = appAuthenticationStateProvider;
    }

    public async Task SignIn(TokenRequestDto dto)
    {
        dto.GrantType = "password";

        var tokenRequest = await _apiCaller.PostAsync<TokenResponseDto, TokenRequestDto>("Auth/GetToken", dto);

        if (tokenRequest is not null)
        {
            await _localStorageService.SetToCookieAsync("access_token", tokenRequest.Data.access_token);
            await _appAuthenticationStateProvider.RaiseAuthenticationStateHasChanged();
        }
    }

    public async Task SignOut()
    {
        await _localStorageService.RemoveFromCookieAsync("access_token");
        await _appAuthenticationStateProvider.RaiseAuthenticationStateHasChanged();
    }
}
