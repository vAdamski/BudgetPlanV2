namespace BudgetPlan.Persistence.Marten.Projections.FinancialEntries;

public sealed class UserFinancialEntriesReadModel
{
    public Guid Id { get; set; }
    public List<FinancialEntryReadModelItem> FinancialEntries { get; set; } = [];
}
