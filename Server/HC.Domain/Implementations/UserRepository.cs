using HC.DataAccess.Context;
using HC.Entity.Identity;
using HC.Service.Implementations;
using HC.Domain.Contracts;
using HC.Shared.Markers;
using HC.Shared.Dtos.Identity;
using HC.Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using HC.Common.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

    public async Task<LoginResponseDto> Login(LoginRequestDto request, CancellationToken cancellationToken)
    {
        if (request.grant_type.Equals("password", StringComparison.OrdinalIgnoreCase) is false) throw new Exception("OAuth flow is not password.");

        var user = await _userManager.FindByNameAsync(request.username);
        if (user == null) throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.password);
        if (!isPasswordValid) throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

        var jwt = await GenerateTokenAsync(user);

        return jwt;
    }

    public Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken)
    {
        user.SecurityStamp = Guid.NewGuid().ToString();
        return UpdateAsync(user, cancellationToken);
    }

    private async Task<LoginResponseDto> GenerateTokenAsync(User user)
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

        return new LoginResponseDto()
        {
            token_type = "Bearer",
            access_token = $"{securityToken.Header}.{securityToken.Payload}.{securityToken.RawSignature}",
            expires_in = securityToken.ValidTo
        };
    }
}
