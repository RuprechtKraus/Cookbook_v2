namespace Cookbook_v2.Toolkit.Exceptions
{
    public class ImageFormatException : AppException
    {
        public ImageFormatException()
            : base()
        {
        }

        public ImageFormatException( string message )
            : base( message )
        {
        }
    }
}
