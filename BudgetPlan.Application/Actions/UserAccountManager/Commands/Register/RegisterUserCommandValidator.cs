using FluentValidation;

namespace BudgetPlan.Application.Actions.UserAccountManager.Commands.Register;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();
        
        RuleFor(x => x.Password)
            .MinimumLength(8)
            .MaximumLength(32)
            .NotEmpty();
        
        RuleFor(x => x.DisplayName)
            .NotEmpty();
    }
}