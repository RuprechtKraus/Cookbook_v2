using System;
using System.Collections.Generic;

namespace Cookbook_v2.Toolkit.Exceptions
{
    public class RegistrationException : AppException
    {
        public IDictionary<string, string[]> RegistrationErrors { get; }

        public RegistrationException()
            : base()
        {
        }

        public RegistrationException( string message )
            : base( message )
        {
        }

        public RegistrationException( IDictionary<string, string[]> errors )
            : base()
        {
            RegistrationErrors = errors;
        }

        public RegistrationException( IDictionary<string, string[]> errors, string message )
            : base(message)
        {
            RegistrationErrors = errors;
        }
    }
}
