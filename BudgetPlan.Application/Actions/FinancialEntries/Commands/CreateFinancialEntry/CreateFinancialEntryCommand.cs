using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.FinancialEntries.Commands.CreateFinancialEntry;

public sealed record CreateFinancialEntryCommand(
    Guid CategoryId,
    Guid SubcategoryId,
    decimal Amount,
    DateOnly OccurredOn) : ICommand<CreateFinancialEntryResult>;
