namespace Mandarin.Idp.Contracts.Models;

/// <summary>
/// Client introspection data
/// </summary>
public class UserIntrospectionRequest
{
    /// <summary>
    /// Application token
    /// </summary>
    public string? ClientToken { get; set; }
        
    /// <summary>
    /// Merchant token
    /// </summary>
    public string? MerchantToken { get; set; }
}