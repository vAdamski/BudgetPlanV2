using BudgetPlan.Domain.Enums;

namespace BudgetPlan.Application.Actions.FinancialEntries.Queries.GetFinancialEntries;

public sealed record FinancialEntryDto(
    Guid Id,
    Guid CategoryId,
    Guid SubcategoryId,
    CategoryType Type,
    decimal Amount,
    DateOnly OccurredOn,
    bool IsDeleted);
