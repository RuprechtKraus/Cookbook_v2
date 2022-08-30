namespace Cookbook_v2.Application.JsonWebTokenUtils
{
    public interface IJwtUtils<TSubject> where TSubject : class
    {
        public string GenerateToken( TSubject subject );
        public string? ValidateToken( string token );
    }
}
