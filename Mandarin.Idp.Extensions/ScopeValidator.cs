using Mandarin.Idp.Contracts.Exceptions;

namespace Mandarin.Idp.Extensions;

/// <summary>
/// Scope validations 
/// </summary>
public static class ScopeValidator
{
    private static bool Validate(this string userScope, string needScope)
    {
        if (string.IsNullOrWhiteSpace(userScope) || string.IsNullOrWhiteSpace(needScope))
            throw new InvalidScopeException(needScope);
            
        return userScope.Contains(needScope);
    }
    
    /// <summary>
    /// Checking for the presence of user scopes
    /// </summary>
    public static bool Validate(this string[]? scopes, string? needScope, bool isThrowException = true)
    {
        if (scopes != null && !string.IsNullOrWhiteSpace(needScope))
            return string.Join(' ', scopes).Validate(needScope);

        if (!isThrowException) return false;
        if (needScope != null)
            throw new InvalidScopeException(needScope);

        return false;
    }
}