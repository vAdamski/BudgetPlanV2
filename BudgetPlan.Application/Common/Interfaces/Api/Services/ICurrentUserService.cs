namespace BudgetPlan.Application.Common.Interfaces.Api.Services;

public interface ICurrentUserService
{
    Guid UserId { get; }
    string Email { get; }
    string DisplayName { get; }
    IReadOnlyList<string> Roles { get; }
    bool IsAuthenticated { get; }
}