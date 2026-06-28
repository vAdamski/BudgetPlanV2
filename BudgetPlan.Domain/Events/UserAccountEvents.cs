namespace BudgetPlan.Domain.Events;

public static class UserAccountEvents
{
    public sealed record UserAccountCreated(
        Guid UserId,
        string Email,
        string DisplayName);
}