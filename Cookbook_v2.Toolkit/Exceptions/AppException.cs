using System;

namespace Cookbook_v2.Toolkit.Exceptions
{
    public class AppException : Exception
    {
        public AppException()
            : base()
        {
        }

        public AppException(string message)
            : base(message)
        {
        }
    }
}
