namespace Mandarin.Idp.Contracts.Models;

public class OAuth2Options
{
    public string Authority { get; set; } = null!;
    public string AuthorizeUrl { get; set; } = null!;
    public string CallbackUrl { get; set; } = null!;
    public ApplicationOption? Idp { get; set; }
    public ApplicationOption? Introspection { get; set; } = null!;
}