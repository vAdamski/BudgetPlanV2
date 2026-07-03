namespace BudgetPlan.Contracts.ControllerContracts.SettlementPeriods.UpdateSettlementPeriod;

public sealed record UpdateSettlementPeriodRequest(
    string Name,
    string Currency,
    DateOnly StartDate,
    DateOnly EndDate);
