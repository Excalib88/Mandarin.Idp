namespace Mandarin.Idp.Contracts;

/// <summary>
/// OAuth2 authentication service
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Start authentication user
    /// </summary>
    Task<object?> Challenge(string? redirectUri = null);
}