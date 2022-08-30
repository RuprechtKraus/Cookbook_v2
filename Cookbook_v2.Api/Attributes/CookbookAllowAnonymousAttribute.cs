using System;

namespace Cookbook_v2.Api.Authorization.Attributes
{
    [AttributeUsage( AttributeTargets.Method )]
    public class CookbookAllowAnonymousAttribute : Attribute
    {
    }
}
