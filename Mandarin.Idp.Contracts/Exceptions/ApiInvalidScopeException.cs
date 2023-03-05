namespace Mandarin.Idp.Contracts.Exceptions;

public class ApiInvalidScopeException : Exception
{
    public ApiInvalidScopeException(string scope, int? userId = null) : base($"Scope {scope} was not found for user {userId}")
    {
    }
}