using Microsoft.AspNetCore.Mvc;

namespace Mandarin.Idp.Contracts.Models;

public class AuthorizationResponseModel
{
    [FromQuery(Name = "access_token")]
    public string AccessToken { get; set; } = null!;
    
    [FromQuery(Name = "expires_in")]
    public int ExpiresIn { get; set; }
    
    [FromQuery(Name = "token_type")]
    public string? TokenType { get; set; }
    
    public string? Scope { get; set; }
    
    public string? State { get; set; }
    
    [FromQuery(Name = "id_token")]
    public string IdToken { get; set; } = null!;
}