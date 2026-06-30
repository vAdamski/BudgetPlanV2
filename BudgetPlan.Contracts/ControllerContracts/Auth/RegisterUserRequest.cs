namespace BudgetPlan.Contracts.ControllerContracts.Auth;

public sealed record RegisterUserRequest(
    string Email,
    string Password,
    string DisplayName);