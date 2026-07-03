using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.HealthActions.Queries.CheckHealth;

public sealed record CheckHealthQuery : IQuery<CheckHealthResult>;
