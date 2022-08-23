using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Cookbook_v2.Domain.UserModel;

namespace Cookbook_v2.Api.Authorization
{
    [AttributeUsage( AttributeTargets.Class | AttributeTargets.Method )]
    public class CookbookAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization( AuthorizationFilterContext context )
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata
                .OfType<CookbookAllowAnonymousAttribute>()
                .Any();

            if ( allowAnonymous )
            {
                return;
            }

            var user = (User) context.HttpContext.Items[ "User" ];
            if ( user == null )
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
