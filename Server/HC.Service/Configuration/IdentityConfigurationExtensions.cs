using System.Net;
using System.Security.Claims;
using System.Text;
using HC.Common.Enums;
using HC.Common.Exceptions;
using HC.Common.Settings;
using HC.Common.Utilities;
using HC.DataAccess.Context;
using HC.Entity.Identity;
using HC.Service.Contracts;
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
            //identityOptions.SignIn.RequireConfirmedEmail = false;
            //identityOptions.SignIn.RequireConfirmedPhoneNumber = false;

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
            var encryptionKey = Encoding.UTF8.GetBytes(JwtSettings.Get().EncryptKey);

            var validationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero, // default: 5 min
                RequireSignedTokens = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ValidateAudience = true, //default : false
                ValidAudience = JwtSettings.Get().Audience,

                ValidateIssuer = true, //default : false
                ValidIssuer = JwtSettings.Get().Issuer,

                TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey)
            };

            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = validationParameters;
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception != null)
                        throw new AppException(ApiResultStatusCode.UnAuthorized, "Authentication failed.", HttpStatusCode.Unauthorized, context.Exception, null);

                    return Task.CompletedTask;
                },
                OnTokenValidated = async context =>
                {
                    var signInManager = context.HttpContext.RequestServices.GetRequiredService<SignInManager<User>>();
                    var userRepository = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();

                    var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                    if (claimsIdentity.Claims?.Any() != true)
                        context.Fail("This token has no claims.");

                    var securityStamp = claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
                    if (!securityStamp.HasValue())
                        context.Fail("This token has no security stamp");

                    //Find user and token from database and perform your custom validation
                    var userId = claimsIdentity.GetUserId<int>();
                    var user = await userRepository.FindByIdAsync(userId.ToString());

                    //if (user.SecurityStamp != Guid.Parse(securityStamp))
                    //    context.Fail("Token security stamp is not valid.");

                    var validatedUser = await signInManager.ValidateSecurityStampAsync(context.Principal);
                    if (validatedUser == null)
                        context.Fail("Token security stamp is not valid.");

                    if (!user.IsActive)
                        context.Fail("User is not active.");

                    user.LastLoginDate = DateTimeOffset.Now;
                    await userRepository.UpdateAsync(user);
                },
                OnChallenge = context =>
                {
                    if (context.AuthenticateFailure != null)
                        throw new AppException(ApiResultStatusCode.UnAuthorized, "Authenticate failure.", HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);
                    throw new AppException(ApiResultStatusCode.UnAuthorized, "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);

                    //return Task.CompletedTask;
                }
            };
        });
    }

}
