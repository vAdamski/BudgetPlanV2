namespace BudgetPlan.Persistence.Marten.Projections.SettlementPeriods;

public sealed class UserSettlementPeriodsReadModel
{
    public Guid Id { get; set; }
    public List<SettlementPeriodReadModelItem> SettlementPeriods { get; set; } = [];
}
