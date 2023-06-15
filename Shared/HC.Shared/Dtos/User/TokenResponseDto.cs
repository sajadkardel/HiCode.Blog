namespace HC.Shared.Dtos.User;

public class TokenResponseDto
{
    public string access_token { get; set; } = default!;
    public string? refresh_token { get; set; }
    public string token_type { get; set; } = default!;
    public DateTime expires_in { get; set; }
}