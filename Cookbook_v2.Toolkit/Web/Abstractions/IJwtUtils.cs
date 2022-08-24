namespace Cookbook_v2.Toolkit.Web.Abstractions
{
    public interface IJwtUtils<TSubject> where TSubject : class
    {
        public string GenerateToken( TSubject subject );
        public string ValidateToken( string token );
    }
}
