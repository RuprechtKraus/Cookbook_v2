using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Cookbook_v2.Api.HttpResponses;
using Cookbook_v2.Toolkit.Exceptions;

namespace Cookbook_v2.Api.Middleware
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
                case ImageFormatException:
                    response.StatusCode = (int) HttpStatusCode.BadRequest;
                    errorResponse = new ErrorResponse( "Image Format Error", exception.Message );
                    break;
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
                    errorResponse = new ErrorResponse( "Internal Server Error" );
                    break;
            }

            await response.WriteAsync( errorResponse.ToString() );
        }
    }
}
