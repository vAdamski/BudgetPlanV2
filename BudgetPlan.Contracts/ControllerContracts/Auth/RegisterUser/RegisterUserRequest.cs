namespace BudgetPlan.Contracts.ControllerContracts.Auth.RegisterUser;

public sealed record RegisterUserRequest(
    string Email,
    string Password,
    string DisplayName);