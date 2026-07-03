using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.UserAccountManager.Queries.GetCurentUser;

public record GetCurrentUserQuery() : IQuery<GetCurrentUserResult>;