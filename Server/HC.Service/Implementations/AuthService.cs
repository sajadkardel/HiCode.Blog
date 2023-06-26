using HC.Common.Exceptions;
using HC.Common.Settings;
using HC.Data.Entities.User;
using HC.Service.Contracts;
using HC.Shared.Dtos.Auth;
using HC.Shared.Markers;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HC.Service.Implementations;

public class AuthService :  IAuthService, IScopedDependency
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
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
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user is null) throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (isPasswordValid is false) throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

        var accessToken = await GenerateTokenAsync(user);

        return new SignInResponseDto
        {
            access_token = accessToken,
        };
    }

    private async Task<string> GenerateTokenAsync(User user)
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
            IssuedAt = DateTime.Now,
            Audience = JwtSettings.Get().Audience,
            NotBefore = DateTime.Now.AddMinutes(JwtSettings.Get().NotBeforeMinutes),
            Expires = DateTime.Now.AddMinutes(JwtSettings.Get().ExpirationMinutes),
            SigningCredentials = signingCredentials,
            EncryptingCredentials = encryptingCredentials,
            Subject = new ClaimsIdentity(claims)
        });

        string accessToken = tokenHandler.WriteToken(securityToken);

        return accessToken;
    }
}
