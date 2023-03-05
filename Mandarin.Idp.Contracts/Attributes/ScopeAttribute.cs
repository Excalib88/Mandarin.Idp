namespace Mandarin.Idp.Contracts.Attributes;

public class ScopeAttribute : Attribute
{
    public string Scope { get; set; }
    public string? Route { get; set; }

    public ScopeAttribute(string scope, string? route = null)
    {
        Scope = scope;
        Route = route;
    }
}