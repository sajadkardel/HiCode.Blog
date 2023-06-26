using HC.Common.Settings;
using HC.Data.Context;
using HC.Data.Repositories.Implementations;
using HC.Service.Implementations;
using HC.Shared.Dtos;
using HC.Shared.Extensions;
using HC.Shared.Markers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace HC.Api.Startup;

public static class ServiceCollectionExtensions
{
    public static void AddDbContext(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(ConnectionStrings.Get().SqlServer);
            //Tips
            //Automatic client evaluation is no longer supported. This event is no longer generated.
            //This line is no longer needed.
            //.ConfigureWarnings(warning => warning.Throw(RelationalEventId.QueryClientEvaluationWarning));
        });
    }

    public static void AddMinimalMvc(this IServiceCollection services)
    {
        services.AddRazorPages();

        //https://github.com/aspnet/AspNetCore/blob/0303c9e90b5b48b309a78c2ec9911db1812e6bf3/src/Mvc/Mvc/src/MvcServiceCollectionExtensions.cs
        services.AddControllersWithViews(options =>
        {
            options.Filters.Add(new AuthorizeFilter()); //Apply AuthorizeFilter as global filter to all actions

            //Like [ValidateAntiforgeryToken] attribute but dose not validatie for GET and HEAD http method
            //You can ingore validate by using [IgnoreAntiforgeryToken] attribute
            //Use this filter when use cookie 
            //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());

            //options.UseYeKeModelBinder();
        });
    }

    public static void AddCustomApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
    }

    public static void AddMarkedServices(this IServiceCollection services)
    {
        var sharedAssembly = typeof(ApiResult).Assembly;
        var commonAssembly = typeof(ConnectionStrings).Assembly;
        var dataAccessAssembly = typeof(ApplicationDbContext).Assembly;
        var servicesAssembly = typeof(AuthService).Assembly;
        var apiAssembly = typeof(Program).Assembly;

        services.Scan(scan => scan.FromAssemblies(sharedAssembly, commonAssembly, dataAccessAssembly, servicesAssembly, apiAssembly)
        .AddClasses(classes => classes.AssignableTo<IScopedDependency>())
        .AsImplementedInterfaces()
        .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblies(sharedAssembly, commonAssembly, dataAccessAssembly, servicesAssembly, apiAssembly)
        .AddClasses(classes => classes.AssignableTo<ISingletonDependency>())
        .AsImplementedInterfaces()
        .WithSingletonLifetime());

        services.Scan(scan => scan.FromAssemblies(sharedAssembly, commonAssembly, dataAccessAssembly, servicesAssembly, apiAssembly)
        .AddClasses(classes => classes.AssignableTo<ITransientDependency>())
        .AsImplementedInterfaces()
        .WithTransientLifetime());
    }
}
