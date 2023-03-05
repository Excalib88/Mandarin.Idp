using System.Web;
using Mandarin.Idp.Contracts;
using Mandarin.Idp.Contracts.Exceptions;
using Mandarin.Idp.Contracts.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Mandarin.Idp.Core;

public class IdpIntegrationService : IIdpIntegrationService
{
    private readonly HttpClient _httpClient;
    private readonly OAuth2Options _oAuth2Options;

    public IdpIntegrationService(IHttpClientFactory httpClientFactory, IOptions<OAuth2Options> oAuth2Options)
    {
        _httpClient = httpClientFactory.CreateClient("OAuth2");
        _oAuth2Options = oAuth2Options.Value;
    }
    
    /// <summary>
    /// Receiving token for application
    /// </summary>
    public async Task<ApplicationAuthenticationResult> Authenticate(string scopes)
    {
        var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
        {
            new ("grant_type", "client_credentials"),
            new ("client_id", _oAuth2Options.Introspection!.ClientId),
            new ("client_secret", _oAuth2Options.Introspection!.ClientSecret),
            new ("scope", scopes)
        });
        var response = await _httpClient.PostAsync("/oauth/token/", content);
        if (!response.IsSuccessStatusCode) throw new MerchantAuthenticationException("OAuth2 authentication failed");
        
        var resultContent = await response.Content.ReadAsStringAsync();
        var applicationAuthenticationResult = JsonConvert.DeserializeObject<ApplicationAuthenticationResult>(resultContent);

        return applicationAuthenticationResult!;
    }

    public string AuthorizeUrl(string? redirectUrl, string baseUri)
    {
        var request =  new AuthorizationRequestModel
        {
            ClientId = _oAuth2Options.Idp!.ClientId,
            RedirectUri = $"{baseUri.TrimEnd('/')}/{_oAuth2Options.CallbackUrl.TrimStart('/')}",
            State = redirectUrl,
            Nonce = Guid.NewGuid().ToString(),
            Scope = "openid"
        };
        var deserializedRequest = JsonConvert.DeserializeObject<IDictionary<string, string>>(JsonConvert.SerializeObject(request));
        var queryParamsDictionary = deserializedRequest!.Select(x => 
            HttpUtility.UrlEncode(x.Key) + "=" + HttpUtility.UrlEncode(x.Value));
        var queryParams = string.Join("&", queryParamsDictionary);

        return $"{_oAuth2Options.AuthorizeUrl}?{queryParams}";
    }

    public async Task RevokeToken(string token)
    {
        using var client = new HttpClient{BaseAddress = new Uri(_oAuth2Options.Authority)};
        var request = new FormUrlEncodedContent(new Dictionary<string, string>()
        {
            ["client_id"] = _oAuth2Options.Idp!.ClientId,
            ["token"] = token
        });
        await client.PostAsync("/oauth/revoke_token/", request);
    }

    /// <summary>
    /// User instrospection
    /// </summary>
    public async Task<UserIntrospectionResult> Introspect(UserIntrospectionRequest request)
    {
        var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
        {
            new ("token", request.MerchantToken!.Replace("Bearer ", ""))
        });
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(_oAuth2Options.Authority)
        };
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {request.ClientToken}");

        HttpResponseMessage response;
        try
        {
            response = await httpClient.PostAsync("/oauth/introspect/", content);
        }
        catch (Exception ex)
        {
            throw new Exception($"Request: /oauth/introspect, {ex.Message}");
        }

        if (!response.IsSuccessStatusCode) throw new UnauthorizedAccessException("OAuth2 introspection failed");
        
        var resultContent = await response.Content.ReadAsStringAsync();
        var applicationAuthenticationResult = JsonConvert.DeserializeObject<UserIntrospectionResult>(resultContent);

        return applicationAuthenticationResult!;
    }
}