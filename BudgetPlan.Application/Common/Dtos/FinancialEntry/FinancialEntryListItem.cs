using BudgetPlan.Domain.Enums;

namespace BudgetPlan.Application.Common.Dtos.FinancialEntry;

public sealed class FinancialEntryListItem
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public Guid SubcategoryId { get; set; }
    public CategoryType Type { get; set; }
    public decimal Amount { get; set; }
    public DateOnly OccurredOn { get; set; }
    public bool IsDeleted { get; set; }
}
