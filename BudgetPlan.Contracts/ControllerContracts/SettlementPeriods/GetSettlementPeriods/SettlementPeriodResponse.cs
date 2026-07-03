namespace BudgetPlan.Contracts.ControllerContracts.SettlementPeriods.GetSettlementPeriods;

public sealed record SettlementPeriodResponse(
    Guid Id,
    string Name,
    string Currency,
    DateOnly StartDate,
    DateOnly EndDate);
