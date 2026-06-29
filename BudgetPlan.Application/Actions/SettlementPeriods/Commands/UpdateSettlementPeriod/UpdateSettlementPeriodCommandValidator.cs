using FluentValidation;

namespace BudgetPlan.Application.Actions.SettlementPeriods.Commands.UpdateSettlementPeriod;

public sealed class UpdateSettlementPeriodCommandValidator : AbstractValidator<UpdateSettlementPeriodCommand>
{
    public UpdateSettlementPeriodCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Settlement period ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Settlement period name is required.")
            .MaximumLength(100).WithMessage("Settlement period name must not exceed 100 characters.");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Settlement period currency is required.")
            .Length(3).WithMessage("Settlement period currency must be a 3-letter ISO code.")
            .Matches("^[A-Za-z]{3}$").WithMessage("Settlement period currency must contain only letters.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Settlement period start date is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("Settlement period end date is required.")
            .GreaterThanOrEqualTo(x => x.StartDate)
            .WithMessage("Settlement period end date cannot be earlier than the start date.");
    }
}
