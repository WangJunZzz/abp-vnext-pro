using Newtonsoft.Json;

namespace Lion.AbpPro.BasicManagement.Users.Dtos;

public class GithubAccessTokenResponse
{
    /// <summary>
    /// access_token
    /// </summary>
    [JsonProperty("access_token")]
    public string Access_token { get; set; }

    /// <summary>
    /// scope
    /// </summary>
    [JsonProperty("scope")]
    public string Scope { get; set; }

    /// <summary>
    /// token_type
    /// </summary>
    [JsonProperty("token_type")]
    public string TokenType { get; set; }
}