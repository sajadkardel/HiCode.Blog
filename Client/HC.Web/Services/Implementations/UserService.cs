using HC.Shared.Constants;
using HC.Shared.Dtos;
using HC.Shared.Dtos.User;
using HC.Shared.Markers;
using HC.Web.Models;
using HC.Web.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Text.Json;

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

    public async Task<ClientSideApiResult<SignUpResponseDto>> SignUp(SignUpRequestDto request)
    {
        var response = await _apiCaller.PostAsync<SignUpResponseDto, SignUpRequestDto>($"{ApiRoutingConstants.Auth.ControllerName}/{ApiRoutingConstants.Auth.SignUp}", request);
        return response;
    }

    public async Task<ClientSideApiResult<SignInResponseDto>> SignIn(SignInRequestDto request)
    {
        var response = await _apiCaller.PostAsync<SignInResponseDto, SignInRequestDto>($"{ApiRoutingConstants.Auth.ControllerName}/{ApiRoutingConstants.Auth.SignIn}", request);

        if (response?.IsSuccess ?? false)
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

    public async Task<ClientSideApiResult<List<UserResponseDto>>> GetAll()
    {
        var response = await _apiCaller.GetAsync<List<UserResponseDto>>($"{ApiRoutingConstants.Auth.ControllerName}/{ApiRoutingConstants.Auth.Get}");
        return response;
    }

    public async Task<ClientSideApiResult<UserResponseDto>> GetById(int id)
    {
        var response = await _apiCaller.GetAsync<UserResponseDto>($"{ApiRoutingConstants.Auth.ControllerName}/{ApiRoutingConstants.Auth.GetById}?id={id}");
        return response;
    }
}
