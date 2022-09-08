namespace Cookbook_v2.Toolkit.Exceptions
{
    public class EntityAlreadyExistsException : EntityException
    {
        public EntityAlreadyExistsException()
            : base()
        {
        }

        public EntityAlreadyExistsException( string message )
            : base( message )
        {
        }
    }
}
