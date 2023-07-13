using HC.Shared.Markers;
using HC.Shared.Models;

namespace HC.Web.Startup;

public static class ServiceCollectionExtensions
{
    public static void AddMarkedServices(this IServiceCollection services)
    {
        var sharedAssembly = typeof(ApiResult).Assembly;
        var webAssembly = typeof(Program).Assembly;

        services.Scan(scan => scan.FromAssemblies(sharedAssembly, webAssembly)
        .AddClasses(classes => classes.AssignableTo<IScopedDependency>())
        .AsImplementedInterfaces()
        .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblies(sharedAssembly, webAssembly)
        .AddClasses(classes => classes.AssignableTo<ISingletonDependency>())
        .AsImplementedInterfaces()
        .WithSingletonLifetime());

        services.Scan(scan => scan.FromAssemblies(sharedAssembly, webAssembly)
        .AddClasses(classes => classes.AssignableTo<ITransientDependency>())
        .AsImplementedInterfaces()
        .WithTransientLifetime());
    }
}
