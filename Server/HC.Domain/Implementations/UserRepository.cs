using HC.DataAccess.Context;
using HC.Service.Implementations;
using HC.Domain.Contracts;
using HC.Shared.Markers;
using HC.Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using HC.DataAccess.Entities.User;
using HC.Shared.Dtos.User;
using HC.Common.Settings;
using System.Security.Claims;
using System.Collections.Immutable;
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

    public async Task<List<UserResponseDto>> GetAllUser(CancellationToken cancellationToken)
    {
        var result = await TableNoTracking.ToListAsync(cancellationToken);

        List<UserResponseDto> response = result.Select(x => new UserResponseDto
        {
           UserName = x.UserName,
           FullName = x.FullName,
           Email = x.Email,
           PhoneNumber = x.PhoneNumber,
           Age = x.Age,
           Gender = x.Gender,
           IsActive = x.IsActive,
           LastLoginDate = x.LastLoginDate

        }).ToList();

        return response;
    }

    public async Task<UserResponseDto> GetUserById(CancellationToken cancellationToken, int id)
    {
        var result = await GetByIdAsync(cancellationToken ,id);

        var response = new UserResponseDto
        {
            UserName = result.UserName,
            FullName = result.FullName,
            Email = result.Email,
            PhoneNumber = result.PhoneNumber,
            Age = result.Age,
            Gender = result.Gender,
            IsActive = result.IsActive,
            LastLoginDate = result.LastLoginDate

        };

        return response;
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
