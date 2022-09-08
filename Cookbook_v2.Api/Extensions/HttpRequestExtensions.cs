using Cookbook_v2.Domain.Entities.UserModel;
using Microsoft.AspNetCore.Http;

namespace Cookbook_v2.Api.Extensions
{
    public static class HttpRequestExtensions
    {
        public static User GetActiveUser( this HttpRequest request )
        {
            return (User) request.HttpContext.Items[ "User" ];
        }
    }
}
