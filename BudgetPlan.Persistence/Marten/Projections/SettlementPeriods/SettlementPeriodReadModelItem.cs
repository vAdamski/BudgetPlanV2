namespace BudgetPlan.Persistence.Marten.Projections.SettlementPeriods;

public sealed class SettlementPeriodReadModelItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
