namespace Cookbook_v2.Application.Responses.UserModel
{
    public class AuthenticateUserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

        public AuthenticateUserResponse( int id, string name, string username, string token )
        {
            Id = id;
            Name = name;
            Username = username;
            Token = token;
        }
    }
}
