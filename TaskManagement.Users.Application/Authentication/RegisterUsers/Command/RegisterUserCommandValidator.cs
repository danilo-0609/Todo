using FluentValidation;

namespace TodoManagement.Users.Application.Authentication.RegisterUsers.Command;

internal class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(r => r.Login)
            .NotEmpty().WithMessage("Login is required")
            .MinimumLength(7).WithMessage("Login length must be 7 characters at least")
            .MaximumLength(20).WithMessage("Login length must be shorter than 20 characters");

        RuleFor(r => r.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MinimumLength(2).WithMessage("First name length must be 2 characters at least")
            .MaximumLength(20).WithMessage("First name length must be shorter than 20 characters");

        RuleFor(r => r.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MinimumLength(2).WithMessage("Last name length must be 2 characters at least")
            .MaximumLength(20).WithMessage("Last name length must be shorter than 20 characters");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(7).WithMessage("Password length must be 7 characters at least")
            .MaximumLength(30).WithMessage("Password length must be shorter than 20 characters");
    }
}
