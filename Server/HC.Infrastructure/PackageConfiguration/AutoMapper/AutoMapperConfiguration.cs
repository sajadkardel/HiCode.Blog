using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace HC.Infrastructure.PackageConfiguration.AutoMapper;

public static class AutoMapperConfiguration
{
    public static void InitializeAutoMapper(this IServiceCollection services, params Assembly[] assemblies)
    {
        //With AutoMapper Instance, you need to call AddAutoMapper services and pass assemblies that contains automapper Profile class
        //services.AddAutoMapper(assembly1, assembly2, assembly3);
        //See http://docs.automapper.org/en/stable/Configuration.html
        //And https://code-maze.com/automapper-net-core/

        services.AddAutoMapper(config =>
        {
            config.AddCustomMappingProfile(assemblies);
        });
    }

    public static void AddCustomMappingProfile(this IMapperConfigurationExpression config, params Assembly[] assemblies)
    {
        var allTypes = assemblies.SelectMany(a => a.ExportedTypes);

        var list = allTypes.Where(type => type.IsClass && !type.IsAbstract && type.GetInterfaces()
            .Contains(typeof(IHaveCustomMapping))).Select(type => Activator.CreateInstance(type) as IHaveCustomMapping);

        var profile = new CustomMappingProfile(list);

        config.AddProfile(profile);
    }
}
