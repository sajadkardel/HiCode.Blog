using HC.Web;
using HC.Web.Services.Implementations;
using HC.Web.Settings;
using HC.Web.Shared;
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

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}/{ServerSideSettings.Get().BaseUrl}") });

builder.Services.AddMarkedServices();

await builder.Build().RunAsync();
