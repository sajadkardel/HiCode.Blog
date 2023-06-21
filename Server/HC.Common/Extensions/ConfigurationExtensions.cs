using Microsoft.Extensions.Configuration;

namespace HC.Common.Extensions;

public class ConfigurationExtensions
{
    public static T? GetSection<T>(string appSettingPath, string section)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile(appSettingPath, false, true).Build();
        var sec = configuration.GetSection(section);
        var t = sec.Get<T>();
        return t;
    }
}
