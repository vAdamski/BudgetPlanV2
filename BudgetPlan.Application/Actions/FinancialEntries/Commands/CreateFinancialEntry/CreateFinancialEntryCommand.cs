using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.FinancialEntries.Commands.CreateFinancialEntry;

public sealed class CreateFinancialEntryCommand : ICommand<Guid>
{
    public Guid CategoryId { get; set; }
    public Guid SubcategoryId { get; set; }
    public decimal Amount { get; set; }
    public DateOnly OccurredOn { get; set; }
}
