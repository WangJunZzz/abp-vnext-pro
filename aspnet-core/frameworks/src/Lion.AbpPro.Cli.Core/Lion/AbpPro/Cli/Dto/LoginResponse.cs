using System.Text.Json.Serialization;

namespace Lion.AbpPro.Cli.Dto;

public class LoginResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; }
}