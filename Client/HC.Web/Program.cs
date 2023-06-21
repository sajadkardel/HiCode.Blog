using HC.Web;
using HC.Web.Services.Implementations;
using HC.Web.Startup;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<AppAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<AppAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AppHttpClientHandler>();

builder.Services.AddScoped(sp => new HttpClient(sp.GetRequiredService<AppHttpClientHandler>()) { BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}{builder.Configuration["ServerBaseUrl"]}") });

builder.Services.AddMarkedServices();

await builder.Build().RunAsync();
