using FluentValidation;

namespace BudgetPlan.Application.Actions.FinancialEntries.Commands.DeleteFinancialEntry;

public sealed class DeleteFinancialEntryCommandValidator : AbstractValidator<DeleteFinancialEntryCommand>
{
    public DeleteFinancialEntryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Financial entry ID is required.");
    }
}
