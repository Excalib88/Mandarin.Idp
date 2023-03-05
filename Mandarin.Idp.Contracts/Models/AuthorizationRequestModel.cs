using Newtonsoft.Json;

namespace Mandarin.Idp.Contracts.Models;

public class AuthorizationRequestModel
{
    [JsonProperty("client_id")]
    public string ClientId { get; set; } = null!;

    [JsonProperty("redirect_uri")]
    public string? RedirectUri { get; set; }
    
    [JsonProperty("scope")]
    public string Scope { get; set; } = "openid";
    
    [JsonProperty("response_type")]
    public string ResponseType { get; set; } = "id_token token";
    
    [JsonProperty("response_mode")]
    public string ResponseMode { get; set; } = "query";
    
    [JsonProperty("nonce")]
    public string? Nonce { get; set; }
    
    [JsonProperty("state")]
    public string? State { get; set; }
}