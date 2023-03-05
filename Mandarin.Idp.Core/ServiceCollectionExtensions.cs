using Mandarin.Idp.Contracts;
using Mandarin.Idp.Contracts.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IAuthenticationService = Mandarin.Idp.Contracts.IAuthenticationService;

namespace Mandarin.Idp.Core;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adding introspection services for DI
    /// </summary>
    public static void AddIdpAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDistributedMemoryCache();
        services.AddScoped<IUserProvider, UserProvider>();
        services.AddHttpClient("OAuth2", client =>
        {
            client.BaseAddress = new Uri(configuration["OAuth2:Authority"]);
        });
        services.Configure<OAuth2Options>(_ => configuration.GetSection("OAuth2"));
        services.AddSingleton<IIdpIntegrationService, IdpIntegrationService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddHttpContextAccessor();
    }
}