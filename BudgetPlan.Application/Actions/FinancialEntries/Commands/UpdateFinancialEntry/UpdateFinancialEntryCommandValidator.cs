using FluentValidation;

namespace BudgetPlan.Application.Actions.FinancialEntries.Commands.UpdateFinancialEntry;

public sealed class UpdateFinancialEntryCommandValidator : AbstractValidator<UpdateFinancialEntryCommand>
{
    public UpdateFinancialEntryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Financial entry ID is required.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category ID is required.");

        RuleFor(x => x.SubcategoryId)
            .NotEmpty().WithMessage("Subcategory ID is required.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Financial entry amount must be greater than zero.");

        RuleFor(x => x.OccurredOn)
            .NotEmpty().WithMessage("Financial entry occurrence date is required.");
    }
}
