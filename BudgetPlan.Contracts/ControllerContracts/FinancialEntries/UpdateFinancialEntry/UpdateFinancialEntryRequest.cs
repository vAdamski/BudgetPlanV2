namespace BudgetPlan.Contracts.ControllerContracts.FinancialEntries.UpdateFinancialEntry;

public sealed record UpdateFinancialEntryRequest(
    Guid CategoryId,
    Guid SubcategoryId,
    decimal Amount,
    DateOnly OccurredOn);
