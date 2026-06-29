using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.FinancialEntries.Commands.UpdateFinancialEntry;

public sealed class UpdateFinancialEntryCommand : ICommand
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public Guid SubcategoryId { get; set; }
    public decimal Amount { get; set; }
    public DateOnly OccurredOn { get; set; }
}
