using System;
using System.Collections.Generic;

namespace Cookbook_v2.Toolkit.Exceptions
{
    public class RegistrationException : AppException
    {
        public RegistrationException()
            : base()
        {
        }

        public RegistrationException( string message )
            : base( message )
        {
        }
    }
}
