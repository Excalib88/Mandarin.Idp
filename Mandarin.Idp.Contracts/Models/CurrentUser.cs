namespace Mandarin.Idp.Contracts.Models;

/// <summary>
/// Current merchant user
/// </summary>
public class CurrentUser
{
    /// <summary>
    /// Merchant Access token
    /// </summary>
    public string Token { get; set; } = null!;

    /// <summary>
    /// Merchant scopes
    /// </summary>
    public string? Scope { get; set; }
        
    /// <summary>
    /// Merchant user information
    /// </summary>
    public UserInfo User { get; set; } = null!;
}