using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.SettlementPeriods.Queries.GetSettlementPeriods;

public sealed record GetSettlementPeriodsQuery : IQuery<GetSettlementPeriodsResult>;
