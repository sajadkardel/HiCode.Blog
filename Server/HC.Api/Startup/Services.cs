using HC.Infrastructure.Configurations;

namespace HC.Api.Startup;

public class Services
{
    public static void Add(IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
    {
        services.AddDbContext();

        services.AddCustomIdentity();

        services.AddMinimalMvc();

        services.AddJwtAuthentication();

        services.AddCustomApiVersioning();

        services.AddSwagger();

        services.AddMarkedServices();
    }
}
