using HC.Common.Extensions;

namespace HC.Common.Settings;

public class ConnectionStrings
{
    public string SqlServer { get; set; } = default!;

    public static ConnectionStrings Get()
    {
        var appSettingPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName ?? "", "HC.Api", "appsettings.json");
        ConnectionStrings settings = ConfigurationExtensions.GetSection<ConnectionStrings>(appSettingPath, nameof(ConnectionStrings)) ?? new();
        return settings;
    }
}
