namespace BudgetPlan.Domain.Aggregates.UserAccount;

public class UserAccount : AggregateBase
{
    private UserAccount()
    {
        
    }
    
    public static UserAccount Create()
    {
        var userAccount = new UserAccount();
        
        throw new NotImplementedException();
    }
}