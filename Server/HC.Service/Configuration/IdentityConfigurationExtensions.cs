using System.Net;
using System.Security.Claims;
using System.Text;
using HC.Common.Exceptions;
using HC.Common.Extensions;
using HC.Common.Settings;
using HC.DataAccess.Context;
using HC.DataAccess.Entities.User;
using HC.Service.Contracts;
using HC.Shared.Enums;
using HC.Shared.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace HC.Service.Configuration;

public static class IdentityConfigurationExtensions
{
    public static void AddCustomIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(identityOptions =>
        {
            //Password Settings
            identityOptions.Password.RequireDigit = IdentitySettings.Get().PasswordRequireDigit;
            identityOptions.Password.RequiredLength = IdentitySettings.Get().PasswordRequiredLength;
            identityOptions.Password.RequireNonAlphanumeric = IdentitySettings.Get().PasswordRequireNonAlphanumeric; //#@!
            identityOptions.Password.RequireUppercase = IdentitySettings.Get().PasswordRequireUppercase;
            identityOptions.Password.RequireLowercase = IdentitySettings.Get().PasswordRequireLowercase;

            //UserName Settings
            identityOptions.User.RequireUniqueEmail = IdentitySettings.Get().RequireUniqueEmail;

            //Singin Settings
            identityOptions.SignIn.RequireConfirmedEmail = false;
            identityOptions.SignIn.RequireConfirmedPhoneNumber = false;

            //Lockout Settings
            //identityOptions.Lockout.MaxFailedAccessAttempts = 5;
            //identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //identityOptions.Lockout.AllowedForNewUsers = false;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
    }

    public static void AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            var secretKey = Encoding.UTF8.GetBytes(JwtSettings.Get().SecretKey);

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true, //default : false
                ValidIssuer = JwtSettings.Get().Issuer,

                ValidateAudience = true, //default : false
                ValidAudience = JwtSettings.Get().Audience,

                ValidateIssuerSigningKey = true, //default : false
                IssuerSigningKey = new SymmetricSecurityKey(secretKey)
            };
        });
    }

}
