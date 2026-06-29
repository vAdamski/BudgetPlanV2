using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.FinancialEntries.Commands.DeleteFinancialEntry;

public sealed class DeleteFinancialEntryCommand : ICommand
{
    public Guid Id { get; set; }
}
