using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Cookbook_v2.Toolkit.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Cookbook_v2.Infrastructure.Web.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware( RequestDelegate next )
        {
            _next = next;
        }

        public async Task InvokeAsync( HttpContext context )
        {
            try
            {
                await _next( context );
            }
            catch ( Exception exception )
            {
                await HandleExceptionAsync( context, exception );
            }
        }

        private static async Task HandleExceptionAsync( HttpContext context, Exception exception )
        {
            HttpResponse response = context.Response;
            response.ContentType = "application/json";

            ErrorResponse errorResponse;

            switch ( exception )
            {
                case RegistrationException:
                    response.StatusCode = (int) HttpStatusCode.BadRequest;
                    errorResponse = new ErrorResponse( "Registration Error", exception.Message );
                    break;
                case AuthenticationException:
                    response.StatusCode = (int) HttpStatusCode.BadRequest;
                    errorResponse = new ErrorResponse( "Authentication Error", exception.Message );
                    break;
                case AppException:
                    response.StatusCode = (int) HttpStatusCode.BadRequest;
                    errorResponse = new ErrorResponse( "Application Error", exception.Message );
                    break;
                case KeyNotFoundException:
                    response.StatusCode = (int) HttpStatusCode.NotFound;
                    errorResponse = new ErrorResponse( "Not Found Error", exception.Message );
                    break;
                default:
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    errorResponse = new ErrorResponse( "Internal Server Error", exception.Message );
                    break;
            }

            await response.WriteAsync( errorResponse.ToString() );
        }
    }
}
