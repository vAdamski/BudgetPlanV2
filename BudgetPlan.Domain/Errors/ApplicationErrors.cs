using BudgetPlan.Domain.Common;

namespace BudgetPlan.Domain.Errors;

public static class ApplicationErrors
{
    public static class User
    {
        public static Error ErrorWhileRegisteringUser() =>
            new("User.ErrorWhileRegisteringUser", "An error occurred while registering the user.");
    }
}