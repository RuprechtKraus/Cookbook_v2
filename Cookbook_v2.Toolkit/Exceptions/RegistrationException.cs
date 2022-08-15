using System;

namespace Cookbook.Exceptions
{
    public class RegistrationException : AppException
    {
        public RegistrationException() 
            : base()
        {
        }

        public RegistrationException(string message)
            : base(message)
        {
        }
    }
}
