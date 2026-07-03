using BudgetPlan.Contracts.Enums;

namespace BudgetPlan.Contracts.ControllerContracts.FinancialEntries.GetFinancialEntry;

public sealed record GetFinancialEntryResponse(
    Guid Id,
    Guid CategoryId,
    Guid SubcategoryId,
    CategoryType Type,
    decimal Amount,
    DateOnly OccurredOn,
    bool IsDeleted);
