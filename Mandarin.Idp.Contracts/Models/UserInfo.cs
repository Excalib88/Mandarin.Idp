using Newtonsoft.Json;

namespace Mandarin.Idp.Contracts.Models;

/// <summary>
/// Information about user
/// </summary>
public class UserInfo
{
    /// <summary>
    /// Client name
    /// </summary>
    public string? Name { get; set; }
        
    /// <summary>
    /// Client roles
    /// </summary>
    public string[]? Roles { get; set; }
        
    /// <summary>
    /// Client email
    /// </summary>
    public string? Email { get; set; }
        
    /// <summary>
    /// Client Id
    /// </summary>
    [JsonProperty("client_id")]
    public int ClientId { get; set; }
        
    [JsonProperty("perms")]
    public string[]? Permissions { get; set; }
}