using System.ComponentModel.DataAnnotations;

namespace Cookbook_v2.Api.MessageContracts.UserModel
{
    public class UserAuthenticateDto
    {
        [Required( ErrorMessage = "Username required" )]
        [MinLength( 3, ErrorMessage = "Minimum username length is 3" )]
        [MaxLength( 64, ErrorMessage = "Maximum username length is 64" )]
        public string? Username { get; set; }

        [Required( ErrorMessage = "Password required" )]
        [MinLength( 8, ErrorMessage = "Minimum password length is 8" )]
        [MaxLength( 32, ErrorMessage = "Maximum password length is 32" )]
        public string? Password { get; set; }
    }
}
