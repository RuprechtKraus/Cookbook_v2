using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook_v2.Toolkit.Exceptions
{
    public class EntityException : AppException
    {
        public EntityException()
            : base()
        {
        }

        public EntityException( string message )
            : base( message )
        {
        }
    }
}
