namespace Cookbook_v2.Api.MessageContracts.UserModel
{
    public class UserAuthenticatedResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
