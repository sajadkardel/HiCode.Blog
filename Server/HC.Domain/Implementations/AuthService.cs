using HC.Common.Settings;
using HC.Data.Entities.Identity;
using HC.Domain.Contracts;
using HC.Shared.Dtos.Auth;
using HC.Shared.Markers;
using HC.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HC.Domain.Implementations;

public class AuthService : IAuthService, IScopedDependency
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Result> SignUp(SignUpRequestDto request, CancellationToken cancellationToken = default)
    {
        var existingUser = await _userManager.FindByNameAsync(request.UserName);
        if (existingUser is not null) return Result.Failed("کاربری با این نام کاربری قبلا ثبت شده.");

        var result = await _userManager.CreateAsync(new User() { UserName = request.UserName, }, request.Password);
        if (result.Succeeded is false) return Result.Failed(result.Errors.First().Description);

        return Result.Success();
    }

    public async Task<Result<SignInResponseDto>> SignIn(SignInRequestDto request, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user is null) return Result.Failed<SignInResponseDto>("نام کاربری یا رمز عبور اشتباه است");

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (isPasswordValid is false) return Result.Failed<SignInResponseDto>("نام کاربری یا رمز عبور اشتباه است");

        var accessToken = await GenerateTokenAsync(user);
        if (accessToken.Succeeded is false) return Result.Failed<SignInResponseDto>(accessToken.Message);

        return Result.Success(new SignInResponseDto
        {
            access_token = accessToken.Data,
        });
    }

    private async Task<Result<string>> GenerateTokenAsync(User user)
    {
        byte[] secretKey = Encoding.UTF8.GetBytes(JwtSettings.Get().SecretKey); // longer that 16 character
        SigningCredentials signingCredentials = new(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256);

        byte[] encryptionkey = Encoding.UTF8.GetBytes(JwtSettings.Get().EncryptKey); //must be 16 character
        EncryptingCredentials encryptingCredentials = new(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        var claims = await _userManager.GetClaimsAsync(user);

        JwtSecurityTokenHandler tokenHandler = new();

        JwtSecurityToken securityToken = tokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor
        {
            Issuer = JwtSettings.Get().Issuer,
            IssuedAt = DateTime.UtcNow,
            Audience = JwtSettings.Get().Audience,
            NotBefore = DateTime.UtcNow.AddMinutes(JwtSettings.Get().NotBeforeMinutes),
            Expires = DateTime.UtcNow.AddMinutes(JwtSettings.Get().ExpirationMinutes),
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials,
            Subject = new ClaimsIdentity(claims)
        });

        string accessToken = tokenHandler.WriteToken(securityToken);

        return Result.Success<string>(accessToken);
    }
}
