using Mandarin.Idp.Contracts;
using Mandarin.Idp.Contracts.Models;
using Mandarin.Idp.Extensions;
using Microsoft.AspNetCore.Http;

namespace Mandarin.Idp.Core;


/// <summary>
/// OAuth2 authentication service
/// </summary>
public class AuthenticationService : IAuthenticationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IIdpIntegrationService _idp;
    private readonly UserProvider _userProvider;

    public AuthenticationService(IHttpContextAccessor httpContextAccessor, IIdpIntegrationService idp, UserProvider userProvider)
    {
        _httpContextAccessor = httpContextAccessor;
        _idp = idp;
        _userProvider = userProvider;
    }

    public async Task<object?> Challenge(string? redirectUri = null)
    {
        var authHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"];
        var accessToken = "";
        
        if (authHeader.HasValue && authHeader.Value.ToString().Contains("Bearer"))
        {
            accessToken = authHeader.Value.FirstOrDefault()?.Split(' ')[1];
        }

        if (string.IsNullOrWhiteSpace(accessToken))
        {
            var baseUri = _httpContextAccessor.HttpContext!.BaseUri();
            return _idp.AuthorizeUrl(redirectUri, baseUri);
        }
        
        var clientCredentials = await _idp.Authenticate(Scopes.Introspection);

        if (clientCredentials == null)
        {
            throw new UnauthorizedAccessException("Application failed to authenticate");
        }
        
        var client = await _idp.Introspect(new UserIntrospectionRequest
        {
            ClientToken = clientCredentials.AccessToken,
            MerchantToken = accessToken
        });

        if (client is not {Active: true})
        {
            throw new UnauthorizedAccessException("Failed to authenticate the merchant");
        }
        
        var currentUser = new CurrentUser
        {
            Scope = client.Scope,
            Token = clientCredentials.AccessToken!,
            User = client.UserInfo!
        };

        if (redirectUri != null)
        {
            SetUserSession(currentUser, clientCredentials.ExpiresIn);
        }

        return currentUser;
    }

    private void SetUserSession(CurrentUser? currentUser, int expiresIn)
    {
        _userProvider.SetUser(currentUser);
            
        var userSession = new UserSession
        {
            Permissions = currentUser!.User.Permissions,
            ClientId = currentUser.User.ClientId,
            ExpiresIn = expiresIn,
            Email = currentUser.User.Email
        };
            
        _httpContextAccessor.HttpContext?.Session.Set("User", userSession);
    }
}
