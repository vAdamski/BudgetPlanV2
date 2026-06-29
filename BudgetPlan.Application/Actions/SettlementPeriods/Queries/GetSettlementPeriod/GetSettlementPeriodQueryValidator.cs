using FluentValidation;

namespace BudgetPlan.Application.Actions.SettlementPeriods.Queries.GetSettlementPeriod;

public sealed class GetSettlementPeriodQueryValidator : AbstractValidator<GetSettlementPeriodQuery>
{
    public GetSettlementPeriodQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Settlement period ID is required.");
    }
}
