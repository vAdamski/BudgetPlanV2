namespace BudgetPlan.Application.Common.Dtos.SettlementPeriod;

public sealed class SettlementPeriodListItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
