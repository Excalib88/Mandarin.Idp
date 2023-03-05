using Newtonsoft.Json;

namespace Mandarin.Idp.Contracts.Models;

/// <summary>
/// Application authentication result with client credentials grant type
/// </summary>
public class ApplicationAuthenticationResult
{
    /// <summary>
    /// Application access token
    /// </summary>
    [JsonProperty("access_token")]
    public string? AccessToken { get; set; }
        
    /// <summary>
    /// Expires time in seconds
    /// </summary>
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
        
    /// <summary>
    /// Token type (by default: "Bearer")
    /// </summary>
    [JsonProperty("token_type")]
    public string? TokenType { get; set; }
        
    /// <summary>
    /// Scopes
    /// </summary>
    [JsonProperty("scope")]
    public string? Scope { get; set; }
}