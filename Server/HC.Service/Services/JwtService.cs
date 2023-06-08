using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HC.Common.Markers;
using HC.Common.Settings;
using HC.Entity.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace HC.Service.Services;

public class JwtService : IJwtService, IScopedDependency
{
    private readonly UserManager<User> _signInManager;

    public JwtService(UserManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<AccessToken> GenerateAsync(User user)
    {
        byte[] secretKey = Encoding.UTF8.GetBytes(JwtSettings.Get().SecretKey); // longer that 16 character
        SigningCredentials signingCredentials = new(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        byte[] encryptionkey = Encoding.UTF8.GetBytes(JwtSettings.Get().EncryptKey); //must be 16 character
        EncryptingCredentials encryptingCredentials = new(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        var claims = await _signInManager.GetClaimsAsync(user);

        SecurityTokenDescriptor descriptor = new()
        {
            Issuer = JwtSettings.Get().Issuer,
            Audience = JwtSettings.Get().Audience,
            IssuedAt = DateTime.Now,
            NotBefore = DateTime.Now.AddMinutes(JwtSettings.Get().NotBeforeMinutes),
            Expires = DateTime.Now.AddMinutes(JwtSettings.Get().ExpirationMinutes),
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials,
            Subject = new ClaimsIdentity(claims)
        };

        //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        //JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
        //JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

        JwtSecurityTokenHandler tokenHandler = new();

        JwtSecurityToken securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);

        return new AccessToken(securityToken);
    }
}
