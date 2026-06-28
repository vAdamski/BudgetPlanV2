namespace BudgetPlan.Domain.Events;

public static class UserAccountEvents
{
    public record UserAccountCreated(
        Guid UserId,
        string Email,
        string DisplayName);
}