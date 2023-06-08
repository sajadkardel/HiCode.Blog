using HC.Shared.Markers;

namespace HC.Web.Startup;

public static class ServiceCollectionExtensions
{
    public static void AddMarkedServices(this IServiceCollection services)
    {
        var webAssembly = typeof(Program).Assembly;

        services.Scan(scan => scan.FromAssemblies(webAssembly)
        .AddClasses(classes => classes.AssignableTo<IScopedDependency>())
        .AsImplementedInterfaces()
        .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblies(webAssembly)
        .AddClasses(classes => classes.AssignableTo<ISingletonDependency>())
        .AsImplementedInterfaces()
        .WithSingletonLifetime());

        services.Scan(scan => scan.FromAssemblies(webAssembly)
        .AddClasses(classes => classes.AssignableTo<ITransientDependency>())
        .AsImplementedInterfaces()
        .WithTransientLifetime());
    }
}
