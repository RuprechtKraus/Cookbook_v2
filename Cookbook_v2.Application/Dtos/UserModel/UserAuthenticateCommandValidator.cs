using FluentValidation;
using Cookbook_v2.Toolkit.Validation;
using static Cookbook_v2.Domain.UserModel.User;

namespace Cookbook_v2.Api.MessageContracts.UserModel
{
    public class UserAuthenticateCommandValidator : AbstractValidator<UserAuthenticateDto>
    {
        private static readonly string s_usernameFieldName = nameof( UserRegisterDto.Username );
        private static readonly string s_passwordFieldName = nameof( UserRegisterDto.Password );

        public UserAuthenticateCommandValidator()
        {
            ConfigureUsernameRules();
            ConfigurePasswordRules();
        }

        private void ConfigureUsernameRules()
        {
            RuleFor( x => x.Username )
                .NotEmpty()
                .WithMessage( ValidationMessage.Required( s_usernameFieldName ) );

            When( x => !string.IsNullOrWhiteSpace( x.Username ), () =>
            {
                RuleFor( x => x.Username )
                    .MinimumLength( s_usernameMinLength )
                    .WithMessage( ValidationMessage.MinLength( s_usernameFieldName, s_usernameMinLength ) )
                    .MaximumLength( s_usernameMaxLength )
                    .WithMessage( ValidationMessage.MaxLength( s_usernameFieldName, s_usernameMaxLength ) );
            } );
        }

        private void ConfigurePasswordRules()
        {
            RuleFor( x => x.Password )
                .NotEmpty()
                .WithMessage( ValidationMessage.Required( s_passwordFieldName ) );

            When( x => !string.IsNullOrWhiteSpace( x.Password ), () =>
            {
                RuleFor( x => x.Password )
                    .MinimumLength( s_passwordMinLength )
                    .WithMessage( ValidationMessage.MinLength( s_passwordFieldName, s_passwordMinLength ) )
                    .MaximumLength( s_passwordMaxLength )
                    .WithMessage( ValidationMessage.MaxLength( s_passwordFieldName, s_passwordMaxLength ) );
            } );
        }
    }
}
