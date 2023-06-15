
namespace HC.Web.Settings;

public class ServerSideSettings
{
    public string BaseUrl { get; set; } = default!;

    public static ServerSideSettings Get()
    {
        var appSettingPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName ?? "", "HC.Web", "appsettings.json");
        ServerSideSettings settings = HC.Shared.Extensions.ConfigurationExtensions.GetSection<ServerSideSettings>(appSettingPath, nameof(ServerSideSettings)) ?? new();
        return settings;
    }
}
