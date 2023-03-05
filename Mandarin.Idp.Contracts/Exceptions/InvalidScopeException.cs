namespace Mandarin.Idp.Contracts.Exceptions;

public class InvalidScopeException : Exception
{
    public InvalidScopeException(string scope, int? userId = null) : base($"Scope {scope} was not found for user {userId}")
    {
    }
}