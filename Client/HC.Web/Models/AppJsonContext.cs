using HC.Shared.Dtos.Auth;
using HC.Shared.Dtos.User;
using System.Text.Json.Serialization;

namespace HC.Web.Models;

/// <summary>
/// https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-source-generator/
/// </summary>

// Json Settings
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]

// General
[JsonSerializable(typeof(ClientSideApiResult))]

// SignUp
[JsonSerializable(typeof(SignUpRequestDto))]

// SignIn
[JsonSerializable(typeof(SignInRequestDto))]
[JsonSerializable(typeof(ClientSideApiResult<SignInResponseDto>))]

// User
[JsonSerializable(typeof(UserRequestDto))]
[JsonSerializable(typeof(ClientSideApiResult<UserResponseDto>))]
[JsonSerializable(typeof(ClientSideApiResult<List<UserResponseDto>>))]

public partial class AppJsonContext : JsonSerializerContext
{
}
