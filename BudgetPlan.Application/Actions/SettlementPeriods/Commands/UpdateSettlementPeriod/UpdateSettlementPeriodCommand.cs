using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.SettlementPeriods.Commands.UpdateSettlementPeriod;

public sealed record UpdateSettlementPeriodCommand(
    Guid Id,
    string Name,
    string Currency,
    DateOnly StartDate,
    DateOnly EndDate) : ICommand;
