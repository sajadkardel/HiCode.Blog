
namespace HC.Shared.Dtos.Identity;

public class LoginResponseDto
{
    public string access_token { get; set; }
    public string refresh_token { get; set; }
    public string token_type { get; set; }
    public DateTime expires_in { get; set; }
}
