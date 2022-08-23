using static BCrypt.Net.BCrypt;

namespace Cookbook_v2.Domain.UserModel
{
    public partial class User
    {
        public static class Builder
        {
            public static User CreateUser( string name, string userName, string about, string password )
            {
                return new User
                {
                    Name = name,
                    Username = userName,
                    PasswordHash = HashPassword( password )
                };
            }
        }
    }
}
