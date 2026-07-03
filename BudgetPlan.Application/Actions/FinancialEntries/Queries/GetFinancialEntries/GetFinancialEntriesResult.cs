namespace BudgetPlan.Application.Actions.FinancialEntries.Queries.GetFinancialEntries;

public sealed record GetFinancialEntriesResult(IReadOnlyList<FinancialEntryDto> FinancialEntries);
