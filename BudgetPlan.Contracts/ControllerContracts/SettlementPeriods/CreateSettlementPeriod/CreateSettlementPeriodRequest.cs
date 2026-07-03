namespace BudgetPlan.Contracts.ControllerContracts.SettlementPeriods.CreateSettlementPeriod;

public sealed record CreateSettlementPeriodRequest(
    string Name,
    string Currency,
    DateOnly StartDate,
    DateOnly EndDate);
