using System;
using System.Linq;
using Cookbook_v2.Domain.Entities.UserModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cookbook_v2.Api.Authorization.Attributes
{
    [AttributeUsage( AttributeTargets.Class | AttributeTargets.Method )]
    public class CookbookAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization( AuthorizationFilterContext context )
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata
                .OfType<CookbookAllowAnonymousAttribute>()
                .Any();
            var user = (User) context.HttpContext.Items[ "User" ];

            if ( !allowAnonymous && user == null )
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
