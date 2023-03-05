using Microsoft.AspNetCore.Http;

namespace Mandarin.Idp.Extensions;

public static class HttpContextExtensions
{
    public static string RequestUri(this HttpContext context)
    {
        var requestUri = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}";

        return requestUri;
    }

    public static string BaseUri(this HttpContext context)
    {
        var requestUri = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}";

        return requestUri;
    }
}
