using Autofac;
using HC.Common.Markers;
using HC.Common.Settings;
using HC.DataAccess.Context;
using HC.DataAccess.Contracts;
using HC.DataAccess.Repositories;
using HC.Entity.Common;
using HC.Service.Services;

namespace HC.Infrastructure.PackageConfiguration.Autofac;

public static class AutofacConfigurationExtensions
{
    public static void AddServices(this ContainerBuilder containerBuilder)
    {
        //RegisterType > As > Liftetime
        containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

        var commonAssembly = typeof(GeneralSettings).Assembly;
        var entitiesAssembly = typeof(IEntity).Assembly;
        var dataAssembly = typeof(ApplicationDbContext).Assembly;
        var servicesAssembly = typeof(JwtService).Assembly;

        containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, dataAssembly, servicesAssembly)
            .AssignableTo<IScopedDependency>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, dataAssembly, servicesAssembly)
            .AssignableTo<ITransientDependency>()
            .AsImplementedInterfaces()
            .InstancePerDependency();

        containerBuilder.RegisterAssemblyTypes(commonAssembly, entitiesAssembly, dataAssembly, servicesAssembly)
            .AssignableTo<ISingletonDependency>()
            .AsImplementedInterfaces()
            .SingleInstance();
    }
}
