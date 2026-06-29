using BudgetPlan.Domain.Enums;

namespace BudgetPlan.Persistence.Marten.Projections.FinancialEntries;

public sealed class FinancialEntryReadModelItem
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public Guid SubcategoryId { get; set; }
    public CategoryType Type { get; set; }
    public decimal Amount { get; set; }
    public DateOnly OccurredOn { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}
