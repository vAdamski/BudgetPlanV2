namespace BudgetPlan.Domain.Events;

public static class SettlementPeriodEvents
{
    public sealed record SettlementPeriodCreated(
        Guid SettlementPeriodId,
        Guid UserId,
        string Name,
        string Currency,
        DateOnly StartDate,
        DateOnly EndDate) : IUserId;

    public sealed record SettlementPeriodUpdated(
        Guid SettlementPeriodId,
        Guid UserId,
        string Name,
        string Currency,
        DateOnly StartDate,
        DateOnly EndDate) : IUserId;
}
