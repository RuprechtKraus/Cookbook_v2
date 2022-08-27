namespace Cookbook_v2.Api.MessageContracts.UserModel
{
    public class UserRegisterDto
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
}
