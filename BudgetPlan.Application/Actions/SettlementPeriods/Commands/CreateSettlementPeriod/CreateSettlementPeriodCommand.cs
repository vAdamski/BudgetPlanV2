using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.SettlementPeriods.Commands.CreateSettlementPeriod;

public sealed class CreateSettlementPeriodCommand : ICommand<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
