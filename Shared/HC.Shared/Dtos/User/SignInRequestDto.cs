using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HC.Shared.Dtos.User;

public class SignInRequestDto
{
    [Required]
    [JsonPropertyName("grant_type")]
    public string GrantType { get; set; } = default!;

    [Required]
    [JsonPropertyName("username")]
    public string UserName { get; set; } = default!;

    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; } = default!;

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }

    [JsonPropertyName("client_id")]
    public string? ClientId { get; set; }

    [JsonPropertyName("client_secret")]
    public string? ClientSecret { get; set; }
}
