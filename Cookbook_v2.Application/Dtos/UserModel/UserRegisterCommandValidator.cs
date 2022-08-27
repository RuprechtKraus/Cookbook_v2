using FluentValidation;
using Cookbook_v2.Toolkit.Validation;
using static Cookbook_v2.Domain.UserModel.User;

namespace Cookbook_v2.Api.MessageContracts.UserModel
{
    public class UserRegisterCommandValidator : AbstractValidator<UserRegisterDto>
    {
        private static readonly string s_nameFieldName = nameof( UserRegisterDto.Name );
        private static readonly string s_usernameFieldName = nameof( UserRegisterDto.Username );
        private static readonly string s_aboutFieldName = nameof( UserRegisterDto.About );
        private static readonly string s_passwordFieldName = nameof( UserRegisterDto.Password );
        private static readonly string s_repeatPasswordFieldName = nameof( UserRegisterDto.RepeatPassword );

        public UserRegisterCommandValidator()
        {
            ConfigureNameRules();
            ConfigureUsernameRules();
            ConfigureAboutRules();
            ConfigurePasswordRules();
            ConfigureRepeatPasswordRules();
        }

        private void ConfigureNameRules()
        {
            RuleFor( x => x.Name )
                .NotEmpty()
                .WithMessage( ValidationMessage.Required( s_nameFieldName ) );

            When( x => !string.IsNullOrWhiteSpace( x.Name ), () =>
            {
                RuleFor( x => x.Name )
                    .MaximumLength( s_nameMaxLength )
                    .WithMessage( ValidationMessage.MaxLength( s_nameFieldName, s_nameMaxLength ) );
            } );
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

        private void ConfigureAboutRules()
        {
            RuleFor( x => x.About )
                .MaximumLength( s_aboutMaxLength )
                .WithMessage( ValidationMessage.MaxLength( s_aboutFieldName, s_aboutMaxLength ) );
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

        private void ConfigureRepeatPasswordRules()
        {
            RuleFor( x => x.RepeatPassword )
                .NotEmpty()
                .WithMessage( ValidationMessage.Required( s_repeatPasswordFieldName ) );

            When( x => !string.IsNullOrWhiteSpace( x.RepeatPassword ), () =>
            {
                RuleFor( x => x.RepeatPassword )
                    .Equal( x => x.Password )
                    .WithMessage( ValidationMessage.Equal( s_repeatPasswordFieldName, s_passwordFieldName ) );
            } );
        }
    }
}
