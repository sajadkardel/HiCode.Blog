using HC.Shared.Enums;

namespace HC.Shared.Dtos.Identity;

public class UserSelectDto
{
    public string? UserName { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int? Age { get; set; }
    public GenderType? Gender { get; set; }
    public bool? IsActive { get; set; }
    public DateTimeOffset? LastLoginDate { get; set; }
}
