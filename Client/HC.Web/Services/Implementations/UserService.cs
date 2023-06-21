using HC.Shared.Dtos.User;
using HC.Shared.Markers;
using HC.Web.Models;
using HC.Web.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;

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

    public async Task<ClientSideApiResult> SignUp(SignUpRequestDto request)
    {
        return await _apiCaller.PostAsync("Auth/SignUp", request);
    }

    public async Task<ClientSideApiResult<SignInResponseDto>> SignIn(SignInRequestDto dto)
    {
        var signInResponse = await _apiCaller.PostAsync<SignInResponseDto, SignInRequestDto>("Auth/SignIn", dto);

        if (signInResponse.IsSuccess)
        {
            JwtSecurityTokenHandler tokenHandler = new();

            if (tokenHandler.CanReadToken(signInResponse.Data.access_token))
            {
                var securityToken = tokenHandler.ReadJwtToken(signInResponse.Data.access_token);
                await _localStorageService.SetToCookieAsync("access_token", signInResponse.Data.access_token, (DateTime.Now.Second - securityToken.ValidTo.Second));
                await _appAuthenticationStateProvider.RaiseAuthenticationStateHasChanged();
            }
        }

        return signInResponse;
    }

    public async Task SignOut()
    {
        await _localStorageService.RemoveFromCookieAsync("access_token");
        await _appAuthenticationStateProvider.RaiseAuthenticationStateHasChanged();
    }
}
