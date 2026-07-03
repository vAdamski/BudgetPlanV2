using BudgetPlan.Contracts.Enums;

namespace BudgetPlan.Contracts.ControllerContracts.FinancialEntries.GetFinancialEntries;

public sealed record GetFinancialEntriesRequest
{
    public DateOnly? OccurredFrom { get; init; }

    public DateOnly? OccurredTo { get; init; }

    public Guid? CategoryId { get; init; }

    public Guid? SubcategoryId { get; init; }

    public CategoryType? Type { get; init; }
}
