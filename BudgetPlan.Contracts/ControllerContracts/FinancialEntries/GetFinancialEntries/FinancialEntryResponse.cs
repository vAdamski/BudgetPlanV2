using BudgetPlan.Contracts.Enums;

namespace BudgetPlan.Contracts.ControllerContracts.FinancialEntries.GetFinancialEntries;

public sealed record FinancialEntryResponse(
    Guid Id,
    Guid CategoryId,
    Guid SubcategoryId,
    CategoryType Type,
    decimal Amount,
    DateOnly OccurredOn,
    bool IsDeleted);
