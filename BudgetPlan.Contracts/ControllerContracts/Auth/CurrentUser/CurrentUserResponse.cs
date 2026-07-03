namespace BudgetPlan.Contracts.ControllerContracts.Auth.CurrentUser;

public record CurrentUserResponse(
    Guid Id,
    string Email,
    string DisplayName,
    IReadOnlyCollection<string> Roles);