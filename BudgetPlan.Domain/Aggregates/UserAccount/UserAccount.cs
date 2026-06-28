using BudgetPlan.Domain.Events;

namespace BudgetPlan.Domain.Aggregates.UserAccount;

public class UserAccount : AggregateBase
{
    public string Email { get; private set; }
    public string DisplayName { get; private set; }
    
    private UserAccount()
    {
    }

    public static UserAccount Create(Guid userId, string email, string displayName)
    {
        var userAccount = new UserAccount();

        UserAccountEvents.UserAccountCreated userAccountCreatedEvent =
            new UserAccountEvents.UserAccountCreated(userId, email, displayName);
        
        userAccount.Apply(userAccountCreatedEvent);
        userAccount.AddUncommittedEvent(userAccountCreatedEvent);
        
        return userAccount;
    }

    private void Apply(UserAccountEvents.UserAccountCreated @event)
    {
        Id = @event.UserId;
        Email = @event.Email;
        DisplayName = @event.DisplayName;
        
        Version++;
    }
}