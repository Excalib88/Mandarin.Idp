using Mandarin.Idp.Contracts.Models;

namespace Mandarin.Idp.Contracts;

public interface IIdpIntegrationService
{
    /// <summary>
    /// Receiving token for application
    /// </summary>
    Task<ApplicationAuthenticationResult> Authenticate(string scopes);

    /// <summary>
    /// Url for generating authorize link
    /// </summary>
    string? AuthorizeUrl(string? redirectUrl, string baseUri);

    /// <summary>
    /// Merchant instrospection
    /// </summary>
    Task<UserIntrospectionResult> Introspect(UserIntrospectionRequest request);
}