using FluentValidation;
using Cookbook_v2.Toolkit.Validation;
using static Cookbook_v2.Domain.UserModel.User;

namespace Cookbook_v2.Api.MessageContracts.UserModel
{
    public class UserRegisterCommandValidator : AbstractValidator<UserRegisterCommand>
    {
        private static readonly string s_nameFieldName = nameof( UserRegisterCommand.Name );
        private static readonly string s_usernameFieldName = nameof( UserRegisterCommand.Username );
        private static readonly string s_aboutFieldName = nameof( UserRegisterCommand.About );
        private static readonly string s_passwordFieldName = nameof( UserRegisterCommand.Password );
        private static readonly string s_repeatPasswordFieldName = nameof( UserRegisterCommand.RepeatPassword );

        public UserRegisterCommandValidator()
        {
            RuleFor( x => x.Name )
                .NotEmpty()
                .WithMessage( ValidationMessage.Required( s_nameFieldName ) )
                .MaximumLength( s_nameMaxLength )
                .WithMessage( ValidationMessage.MaxLength( s_nameFieldName, s_nameMaxLength ) );

            RuleFor( x => x.Username )
                .NotEmpty()
                .WithMessage( ValidationMessage.Required( s_usernameFieldName ) )
                .MinimumLength( s_usernameMinLength )
                .WithMessage( ValidationMessage.MinLength( s_usernameFieldName, s_usernameMinLength ) )
                .MaximumLength( s_usernameMaxLength )
                .WithMessage( ValidationMessage.MaxLength( s_usernameFieldName, s_usernameMaxLength ) );

            RuleFor( x => x.About )
                .MaximumLength( s_aboutMaxLength )
                .WithMessage( ValidationMessage.MaxLength( s_aboutFieldName, s_aboutMaxLength ) );

            RuleFor( x => x.Password )
                .NotEmpty()
                .WithMessage( ValidationMessage.Required( s_passwordFieldName ) )
                .MinimumLength( s_passwordMinLength )
                .WithMessage( ValidationMessage.MinLength( s_passwordFieldName, s_passwordMinLength ) )
                .MaximumLength( s_passwordMaxLength )
                .WithMessage( ValidationMessage.MaxLength( s_passwordFieldName, s_passwordMaxLength ) );

            RuleFor( x => x.RepeatPassword )
                .NotEmpty()
                .WithMessage( ValidationMessage.Required( s_repeatPasswordFieldName ) )
                .Equal( x => x.Password )
                .WithMessage( ValidationMessage.Equal( s_repeatPasswordFieldName, s_passwordFieldName ) );
        }
    }
}
