namespace Mandarin.Idp.Contracts.Models;

public class UserSession
{
    public int ClientId { get; set; }
    public int ExpiresIn { get; set; }
    public string? Email { get; set; }
    public string[]? Permissions { get; set; }
}