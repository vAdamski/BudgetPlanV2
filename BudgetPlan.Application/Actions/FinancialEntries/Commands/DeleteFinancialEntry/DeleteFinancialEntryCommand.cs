using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.FinancialEntries.Commands.DeleteFinancialEntry;

public sealed record DeleteFinancialEntryCommand(Guid Id) : ICommand;
