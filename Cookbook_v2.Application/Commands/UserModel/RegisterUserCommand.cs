using System.ComponentModel.DataAnnotations;

namespace Cookbook_v2.Application.Commands.UserModel
{
    public class RegisterUserCommand
    {
        [Required( ErrorMessage = "Name required" )]
        [MaxLength( 128, ErrorMessage = "Maximum name length is 128" )]
        public string? Name { get; set; }

        [Required( ErrorMessage = "Username required" )]
        [MinLength( 3, ErrorMessage = "Minimum username length is 3" )]
        [MaxLength( 64, ErrorMessage = "Maximum username length is 64" )]
        public string? Username { get; set; }

        [Required( ErrorMessage = "Password required" )]
        [MinLength( 8, ErrorMessage = "Minimum password length is 8" )]
        [MaxLength( 32, ErrorMessage = "Maximum password length is 32" )]
        public string? Password { get; set; }

        [Required( ErrorMessage = "Repeated password required" )]
        [Compare( "Password", ErrorMessage = "Passwords don't match" )]
        public string? RepeatPassword { get; set; }
    }
}
