using HC.Infrastructure.Configuration;
using HC.Infrastructure.PackageConfiguration.AutoMapper;
using HC.Infrastructure.PackageConfiguration.Identity;
using HC.Infrastructure.PackageConfiguration.Swagger;

namespace HC.Api.Startup;

public class Services
{
    public static void Add(IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
    {
        services.InitializeAutoMapper();

        services.AddDbContext();

        services.AddCustomIdentity();

        services.AddMinimalMvc();

        services.AddJwtAuthentication();

        services.AddCustomApiVersioning();

        services.AddSwagger();
    }
}
