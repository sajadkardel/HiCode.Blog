using HC.Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HC.Web.Services.Implementations;

public class AppAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IStorageService _localStorageService;

    public AppAuthenticationStateProvider(IStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorageService.GetFromCookieAsync("access_token");

        if (token.Succeeded is false) return NotSignedInUser();

        if (string.IsNullOrWhiteSpace(token.Data)) return NotSignedInUser();

        return SignedInUser(token.Data);
    }

    public async Task RaiseAuthenticationStateHasChanged()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(await GetAuthenticationStateAsync()));
    }

    private AuthenticationState NotSignedInUser()
    {
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    private AuthenticationState SignedInUser(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        ClaimsIdentity identity = new();

        if (tokenHandler.CanReadToken(token))
        {
            JwtSecurityToken jwtSecurityToken = tokenHandler.ReadJwtToken(token);
            identity = new ClaimsIdentity(claims: jwtSecurityToken.Claims, authenticationType: "Bearer", nameType: "name", roleType: "role");
        }

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }
}
