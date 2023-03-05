using Mandarin.Idp.Contracts;
using Mandarin.Idp.Contracts.Attributes;
using Mandarin.Idp.Contracts.Models;
using Mandarin.Idp.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mandarin.Idp.Core;

public class BearerAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserProvider _userProvider;

        public BearerAuthorizeAttribute(IAuthenticationService authenticationService, IUserProvider userProvider)
        {
            _authenticationService = authenticationService;
            _userProvider = userProvider;
        }
        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                var scopeAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttributes(true)
                    .OfType<ScopeAttribute>().FirstOrDefault();
                object challengeResult;
                
                try
                {
                    challengeResult = _authenticationService.Challenge().GetAwaiter().GetResult()!;
                }
                catch
                {
                    context.Result = new ObjectResult(new
                    {
                        error = "Authorization failed. Please check your Authorization Bearer header and try again."
                    })
                    {
                        StatusCode = 401
                    };
                    return;
                }

                if (challengeResult is not CurrentUser user)
                {
                    context.Result = new ObjectResult(new
                    {
                        error = "Authorization failed. Please check your Authorization Bearer header and try again."
                    })
                    {
                        StatusCode = 401
                    };
                    return;
                }

                _userProvider.SetUser(user);

                var isValidated = user.User.Permissions.Validate(scopeAttribute?.Scope!);

                if (!isValidated)
                {
                    context.Result = new ObjectResult(new
                    {
                        error = $"Scope {scopeAttribute?.Scope} was not found for user {user.User.ClientId}"
                    })
                    {
                        StatusCode = 403
                    };
                }
            }
        }
    }