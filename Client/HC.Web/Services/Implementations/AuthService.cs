using HC.Shared.Constants;
using HC.Shared.Dtos.Auth;
using HC.Shared.Markers;
using HC.Shared.Models;
using HC.Web.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;

namespace HC.Web.Services.Implementations;

public class AuthService : IAuthService, IScopedDependency
{
    private readonly IApiCaller _apiCaller;
    private readonly IStorageService _localStorageService;
    private readonly AppAuthenticationStateProvider _appAuthenticationStateProvider;

    public AuthService(IStorageService localStorageService, AppAuthenticationStateProvider appAuthenticationStateProvider, IApiCaller apiCaller)
    {
        _localStorageService = localStorageService;
        _appAuthenticationStateProvider = appAuthenticationStateProvider;
        _apiCaller = apiCaller;
    }

    public async Task<ApiResult<SignUpResponseDto>> SignUp(SignUpRequestDto request)
    {
        var response = await _apiCaller.PostAsync<SignUpResponseDto, SignUpRequestDto>(RoutingConstants.ServerSide.Auth.SignUp, request);
        return response;
    }

    public async Task<ApiResult<SignInResponseDto>> SignIn(SignInRequestDto request)
    {
        var response = await _apiCaller.PostAsync<SignInResponseDto, SignInRequestDto>(RoutingConstants.ServerSide.Auth.SignIn, request);

        if (response?.IsSucceed ?? false)
        {
            JwtSecurityTokenHandler tokenHandler = new();

            if (tokenHandler.CanReadToken(response.Data.access_token))
            {
                var securityToken = tokenHandler.ReadJwtToken(response.Data.access_token);
                await _localStorageService.SetToCookieAsync("access_token", response.Data.access_token, (DateTime.Now.Second - securityToken.ValidTo.Second));
                await _appAuthenticationStateProvider.RaiseAuthenticationStateHasChanged();
            }
        }

        return response;
    }

    public async Task SignOut()
    {
        await _localStorageService.RemoveFromCookieAsync("access_token");
        await _appAuthenticationStateProvider.RaiseAuthenticationStateHasChanged();
    }
}
