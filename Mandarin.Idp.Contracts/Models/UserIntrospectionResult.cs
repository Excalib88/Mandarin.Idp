using Newtonsoft.Json;

namespace Mandarin.Idp.Contracts.Models;

/// <summary>
/// Merchant introspection result
/// </summary>
public class UserIntrospectionResult
{
    /// <summary>
    /// User's token introspected
    /// </summary>
    public bool Active { get; set; }
        
    /// <summary>
    /// User scopes
    /// </summary>
    public string? Scope { get; set; }
        
    //todo: check type
    /// <summary>
    /// Expiration time
    /// </summary>
    public double Exp { get; set; }
        
    /// <summary>
    /// Merchant's client Id
    /// </summary>
    [JsonProperty("client_id")]
    public string? ClientId { get; set; }
        
    /// <summary>
    /// Information about merchant
    /// </summary>
    [JsonProperty("user")]
    public UserInfo? UserInfo { get; set; }
}