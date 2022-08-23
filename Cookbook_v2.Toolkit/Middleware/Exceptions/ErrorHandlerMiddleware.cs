using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Cookbook_v2.Toolkit.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Cookbook_v2.Toolkit.Middleware.Exceptions
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
            catch ( Exception error )
            {
                HttpResponse response = context.Response;
                response.ContentType = "application/json";

                string result;

                switch ( error )
                {
                    case RegistrationException e:
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        result = JsonSerializer.Serialize( new
                        {
                            message = e?.Message,
                            errors = e?.RegistrationErrors
                        } );
                        break;
                    case AppException:
                        response.StatusCode = (int) HttpStatusCode.BadRequest;
                        result = JsonSerializer.Serialize( new { message = error?.Message } );
                        break;
                    case KeyNotFoundException:
                        response.StatusCode = (int) HttpStatusCode.NotFound;
                        result = JsonSerializer.Serialize( new { message = error?.Message } );
                        break;
                    default:
                        response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        result = JsonSerializer.Serialize( new { message = error?.Message } );
                        break;
                }

                await response.WriteAsync( result );
            }
        }
    }
}
