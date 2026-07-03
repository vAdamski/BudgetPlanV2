namespace BudgetPlan.Contracts.ControllerContracts.SettlementPeriods.GetSettlementPeriod;

public sealed record GetSettlementPeriodResponse(
    Guid Id,
    string Name,
    string Currency,
    DateOnly StartDate,
    DateOnly EndDate);
