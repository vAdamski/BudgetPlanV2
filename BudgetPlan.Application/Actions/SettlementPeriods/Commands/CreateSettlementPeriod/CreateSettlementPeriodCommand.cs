using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.SettlementPeriods.Commands.CreateSettlementPeriod;

public sealed record CreateSettlementPeriodCommand(
    string Name,
    string Currency,
    DateOnly StartDate,
    DateOnly EndDate) : ICommand<CreateSettlementPeriodResult>;
