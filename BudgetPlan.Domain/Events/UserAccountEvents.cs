namespace BudgetPlan.Domain.Events;

public static class UserAccountEvents
{
    public record UserAccountCreated(
        Guid UserId,
        string AuthenticationSubject,
        string DisplayName,
        string DefaultCurrency);

    public record UserDisplayNameChanged(
        Guid UserId,
        string DisplayName);

    public record UserDefaultCurrencyChanged(
        Guid UserId,
        string Currency);

    public record UserAccountDeactivated(
        Guid UserId,
        DateTimeOffset DeactivatedAt);
}