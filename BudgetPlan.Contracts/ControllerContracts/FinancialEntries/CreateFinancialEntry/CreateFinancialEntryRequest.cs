namespace BudgetPlan.Contracts.ControllerContracts.FinancialEntries.CreateFinancialEntry;

public sealed record CreateFinancialEntryRequest(
    Guid CategoryId,
    Guid SubcategoryId,
    decimal Amount,
    DateOnly OccurredOn);
