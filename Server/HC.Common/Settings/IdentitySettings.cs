using HC.Common.Extensions;

namespace HC.Common.Settings;

public class IdentitySettings
{
    public bool PasswordRequireDigit { get; set; }
    public int PasswordRequiredLength { get; set; }
    public bool PasswordRequireNonAlphanumeric { get; set; }
    public bool PasswordRequireUppercase { get; set; }
    public bool PasswordRequireLowercase { get; set; }
    public bool RequireUniqueEmail { get; set; }

    public static IdentitySettings Get() => ConfigurationExtensions.GetSection<IdentitySettings>(nameof(IdentitySettings));
}
