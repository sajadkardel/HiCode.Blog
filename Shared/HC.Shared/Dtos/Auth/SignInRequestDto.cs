using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HC.Shared.Dtos.Auth;

public class SignInRequestDto
{
    [Required(ErrorMessage = "لطفا نام کاربری را وارد نمایید.")]
    [JsonPropertyName("username")]
    public string UserName { get; set; } = default!;

    [Required(ErrorMessage = "لطفا پسورد را وارد نمایید.")]
    [JsonPropertyName("password")]
    public string Password { get; set; } = default!;
}
