namespace BudgetPlan.Application.Actions.UserAccountManager.Queries.GetCurentUser;

public record GetCurrentUserResult(Guid Id, string Email, string DisplayName, IReadOnlyCollection<string> Roles);