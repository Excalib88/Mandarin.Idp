using Mandarin.Idp.Contracts;
using Mandarin.Idp.Contracts.Attributes;
using Mandarin.Idp.Contracts.Models;
using Mandarin.Idp.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mandarin.Idp.Core;

public class IdpAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly IAuthenticationService _authenticationService;

    public IdpAuthorizeAttribute(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
        
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
        {
            var scopeAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttributes(true).OfType<ScopeAttribute>().FirstOrDefault();
            var userSession = context.HttpContext.Session.Get<UserSession>("User");
                
            if (userSession == null)
            {
                var redirectUri = context.HttpContext.RequestUri();
                    
                if (!string.IsNullOrWhiteSpace(scopeAttribute?.Route))
                {
                    redirectUri = context.HttpContext.BaseUri().TrimEnd('/') + scopeAttribute.Route;
                }
                    
                var authenticationResult = _authenticationService.Challenge(redirectUri).GetAwaiter().GetResult();

                if (authenticationResult is not CurrentUser)
                {
                    context.Result = new RedirectResult(authenticationResult?.ToString());
                }
                    
                return;
            }

            var isValidated = userSession.Permissions.Validate(scopeAttribute?.Scope);

            if (!isValidated)
            {
                context.Result = new RedirectResult("/forbidden");
            }
        }
    }
}