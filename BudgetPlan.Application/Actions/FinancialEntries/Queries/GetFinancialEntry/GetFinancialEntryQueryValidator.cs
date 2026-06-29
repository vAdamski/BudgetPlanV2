using FluentValidation;

namespace BudgetPlan.Application.Actions.FinancialEntries.Queries.GetFinancialEntry;

public sealed class GetFinancialEntryQueryValidator : AbstractValidator<GetFinancialEntryQuery>
{
    public GetFinancialEntryQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Financial entry ID is required.");
    }
}
