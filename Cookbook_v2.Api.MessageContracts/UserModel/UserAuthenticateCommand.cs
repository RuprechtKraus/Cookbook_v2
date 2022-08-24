namespace Cookbook_v2.Api.MessageContracts.UserModel
{
    public class UserAuthenticateCommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
