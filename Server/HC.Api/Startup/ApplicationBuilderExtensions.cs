using HC.DataAccess.Context;
using HC.Domain.Contracts;
using HC.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HC.Api.Startup;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder app)
    {
        Assert.NotNull(app, nameof(app));

        //Use C# 8 using variables
        using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>(); //Service locator

        //Dos not use Migrations, just Create Database with latest changes
        //dbContext.Database.EnsureCreated();
        //Applies any pending migrations for the context to the database like (Update-Database)
        dbContext.Database.Migrate();

        var dataInitializers = scope.ServiceProvider.GetServices<IDataInitializer>();
        foreach (var dataInitializer in dataInitializers)
            dataInitializer.InitializeData();

        return app;
    }
}
