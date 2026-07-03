using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.FinancialEntries.Commands.UpdateFinancialEntry;

public sealed record UpdateFinancialEntryCommand(
    Guid Id,
    Guid CategoryId,
    Guid SubcategoryId,
    decimal Amount,
    DateOnly OccurredOn) : ICommand;
