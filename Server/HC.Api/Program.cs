
var builder = WebApplication.CreateBuilder(args);

HC.Api.Startup.Services.Add(builder.Services, builder.Environment, builder.Configuration);

var app = builder.Build();

HC.Api.Startup.Middlewares.Use(app, builder.Environment, builder.Configuration);

app.Run();
