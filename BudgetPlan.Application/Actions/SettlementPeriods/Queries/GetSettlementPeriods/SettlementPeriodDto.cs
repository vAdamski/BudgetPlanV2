namespace BudgetPlan.Application.Actions.SettlementPeriods.Queries.GetSettlementPeriods;

public sealed record SettlementPeriodDto(
    Guid Id,
    string Name,
    string Currency,
    DateOnly StartDate,
    DateOnly EndDate);
