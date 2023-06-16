
namespace HC.Shared.Dtos.User;

public class SignUpRequestDto
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public bool TermsAccepted { get; set; }
}
