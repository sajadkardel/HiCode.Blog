using HC.Common.Extensions;

namespace HC.Common.Settings;

public class JwtSettings
{
    public string SecretKey { get; set; } = default!;
    public string EncryptKey { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public int NotBeforeMinutes { get; set; } = default!;
    public int ExpirationMinutes { get; set; } = default!;

    public static JwtSettings Get()
    {
        var appSettingPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName ?? "", "HC.Api", "appsettings.json");
        JwtSettings settings = ConfigurationExtensions.GetSection<JwtSettings>(appSettingPath, nameof(JwtSettings)) ?? new();
        return settings;
    }
}
