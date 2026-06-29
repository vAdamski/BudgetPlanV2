using BudgetPlan.Application.Actions.SettlementPeriods.Queries.GetSettlementPeriods;
using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.SettlementPeriods.Queries.GetSettlementPeriod;

public sealed record GetSettlementPeriodQuery(Guid Id) : IQuery<SettlementPeriodDto>;
