namespace Mandarin.Idp.Contracts.Exceptions;

public class MerchantAuthenticationException : Exception
{
    public MerchantAuthenticationException(string message = "") : base(message)
    {
    }
}