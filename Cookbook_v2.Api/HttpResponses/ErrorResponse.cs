using System.Text.Json;

namespace Cookbook_v2.Api.HttpResponses
{
    public class ErrorResponse
    {
        public string Error { get; }
        public string Message { get; }

        public ErrorResponse( string error, string message )
        {
            Error = error;
            Message = message;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize( this );
        }
    }
}
