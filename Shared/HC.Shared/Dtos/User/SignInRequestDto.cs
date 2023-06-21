using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HC.Shared.Dtos.User;

public class SignInRequestDto
{
    [Required]
    [JsonPropertyName("username")]
    public string UserName { get; set; } = default!;

    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; } = default!;

    [Required]
    [JsonPropertyName("client_id")]
    public string? ClientId { get; set; }

    [Required]
    [JsonPropertyName("client_secret")]
    public string? ClientSecret { get; set; }
}
