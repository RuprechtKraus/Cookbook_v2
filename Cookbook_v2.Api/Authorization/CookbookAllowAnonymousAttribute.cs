using System;

namespace Cookbook_v2.Api.Authorization
{
    [AttributeUsage( AttributeTargets.Method )]
    public class CookbookAllowAnonymousAttribute : Attribute
    {
    }
}
