using FluentValidation;

namespace BudgetPlan.Application.Actions.FinancialEntries.Queries.GetFinancialEntries;

public sealed class GetFinancialEntriesQueryValidator : AbstractValidator<GetFinancialEntriesQuery>
{
    public GetFinancialEntriesQueryValidator()
    {
        RuleFor(x => x.OccurredTo)
            .GreaterThanOrEqualTo(x => x.OccurredFrom)
            .When(x => x.OccurredFrom.HasValue && x.OccurredTo.HasValue)
            .WithMessage("Financial entry occurrence end date cannot be earlier than the start date.");

        RuleFor(x => x.Type)
            .IsInEnum()
            .When(x => x.Type.HasValue);
    }
}
