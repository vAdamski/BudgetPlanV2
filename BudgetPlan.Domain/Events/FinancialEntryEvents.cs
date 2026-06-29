using BudgetPlan.Domain.Enums;

namespace BudgetPlan.Domain.Events;

public static class FinancialEntryEvents
{
    public sealed record FinancialEntryCreated(
        Guid FinancialEntryId,
        Guid UserId,
        Guid CategoryId,
        Guid SubcategoryId,
        CategoryType Type,
        decimal Amount,
        DateOnly OccurredOn) : IUserId;

    public sealed record FinancialEntryUpdated(
        Guid FinancialEntryId,
        Guid UserId,
        Guid CategoryId,
        Guid SubcategoryId,
        CategoryType Type,
        decimal Amount,
        DateOnly OccurredOn) : IUserId;

    public sealed record FinancialEntryDeleted(
        Guid FinancialEntryId,
        Guid UserId,
        DateTime DeletedAt) : IUserId;
}
