using Microsoft.Extensions.Configuration;

namespace HC.Common.Utilities;

public class ConfigurationExtensions
{
    public static IConfigurationRoot GetConfiguration()
    {
        var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName ?? "";
        var fullPath = Path.Combine(parentDirectory, "HC.Api", "appsettings.json");

        var configuration = new ConfigurationBuilder()
            .AddJsonFile(fullPath, false, true)
            .Build();
        return configuration;
    }

    public static T GetSection<T>(string section)
    {
        var configuration = GetConfiguration();
        var sec = configuration.GetSection(section);
        var t = sec.Get<T>();
        return t;
    }
}
