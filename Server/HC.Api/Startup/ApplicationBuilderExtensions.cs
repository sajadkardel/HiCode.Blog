using HC.Data.Context;
using HC.Data.Services.Contracts;
using HC.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HC.Api.Startup;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder app)
    {
        Assert.NotNull(app, nameof(app));

        using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
        var dataInitializers = scope.ServiceProvider.GetServices<IDataInitializer>();
        foreach (var dataInitializer in dataInitializers) dataInitializer.InitializeData();

        return app;
    }
}
