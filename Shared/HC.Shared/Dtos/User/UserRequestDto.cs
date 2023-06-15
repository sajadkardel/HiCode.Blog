using HC.Shared.Enums;

namespace HC.Shared.Dtos.User;

public class UserRequestDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public int Age { get; set; }
    public GenderType Gender { get; set; }
}