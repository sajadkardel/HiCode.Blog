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
using Microsoft.EntityFrameworkCore;

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

    public async Task SignUp(SignUpRequestDto request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByNameAsync(request.UserName);
        if (existingUser is not null) throw new BadRequestException("کاربری با این نام کاربری قبلا ثبت شده.");

        var result = await _userManager.CreateAsync(new User() { UserName = request.UserName, }, request.Password);

        if (result.Succeeded is false) throw new BadRequestException(result.Errors.First().Description);
    }

    public async Task<SignInResponseDto> SignIn(SignInRequestDto request, CancellationToken cancellationToken)
    {
        if (request.GrantType.ToLower() != JwtSettings.Get().GrantType) throw new Exception("OAuth flow is not password.");

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

    private async Task<SignInResponseDto> GenerateTokenAsync(User user)
    {
        byte[] secretKey = Encoding.UTF8.GetBytes(JwtSettings.Get().SecretKey); // longer that 16 character
        SigningCredentials signingCredentials = new(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

        //byte[] encryptionkey = Encoding.UTF8.GetBytes(JwtSettings.Get().EncryptKey); //must be 16 character
        //EncryptingCredentials encryptingCredentials = new(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        var claims = await _userManager.GetClaimsAsync(user);

        JwtSecurityToken securityToken = new(
            issuer: JwtSettings.Get().Issuer,
            audience: JwtSettings.Get().Audience,
            signingCredentials: signingCredentials,
            expires: DateTime.Now.AddMinutes(JwtSettings.Get().ExpirationMinutes),
            notBefore: DateTime.Now.AddMinutes(JwtSettings.Get().NotBeforeMinutes),
            claims: claims
            );

        string accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return new SignInResponseDto()
        {
            token_type = "Bearer",
            access_token = accessToken,
            expires_in = securityToken.ValidTo
        };
    }
}
