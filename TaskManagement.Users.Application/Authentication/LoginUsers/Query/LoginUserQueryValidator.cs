using FluentValidation;

namespace TodoManagement.Users.Application.Authentication.LoginUsers.Query;

internal sealed class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
{
    public LoginUserQueryValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("The email is required")
            .EmailAddress().WithMessage("The email address is not valid");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("The password is required");
    }
}
