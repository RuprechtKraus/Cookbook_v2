using Cookbook_v2.Toolkit.Exceptions;

namespace Cookbook_v2.Toolkit.Exceptions
{ 
    public class AuthenticationException : AppException
    {
        public AuthenticationException()
            : base()
        {
        }

        public AuthenticationException( string message )
            : base( message )
        {
        }
    }
}
