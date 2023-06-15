using HC.DataAccess.Context;
using HC.Service.Implementations;
using HC.Domain.Contracts;
using HC.Shared.Markers;
using HC.Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HC.DataAccess.Entities.User;
using HC.Shared.Dtos.User;
using HC.Common.Settings;

namespace HC.Domain.Implementations;

public class UserRepository : Repository<User>, IUserRepository, IScopedDependency
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserRepository(ApplicationDbContext dbContext, UserManager<User> userManager, SignInManager<User> signInManager) : base(dbContext)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<TokenResponseDto> GetToken(TokenRequestDto request, CancellationToken cancellationToken)
    {
        if (request.GrantType.Equals(JwtSettings.Get().GrantType, StringComparison.OrdinalIgnoreCase) is false) throw new Exception("OAuth flow is not password.");

        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user is null) throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (isPasswordValid is false) throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

        var jwt = await GenerateTokenAsync(user);

        return jwt;
    }

    public Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken)
    {
        user.SecurityStamp = Guid.NewGuid().ToString();
        return UpdateAsync(user, cancellationToken);
    }

    private async Task<TokenResponseDto> GenerateTokenAsync(User user)
    {
        byte[] secretKey = Encoding.UTF8.GetBytes(JwtSettings.Get().SecretKey); // longer that 16 character
        SigningCredentials signingCredentials = new(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        byte[] encryptionkey = Encoding.UTF8.GetBytes(JwtSettings.Get().EncryptKey); //must be 16 character
        EncryptingCredentials encryptingCredentials = new(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        var claims = await _userManager.GetClaimsAsync(user);

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

        return new TokenResponseDto()
        {
            token_type = "Bearer",
            //access_token = securityToken,
            expires_in = securityToken.ValidTo
        };
    }
}
