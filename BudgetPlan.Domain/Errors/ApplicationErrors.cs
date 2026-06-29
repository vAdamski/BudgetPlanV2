using BudgetPlan.Domain.Common;

namespace BudgetPlan.Domain.Errors;

public static class ApplicationErrors
{
    public static class User
    {
        public static Error ErrorWhileRegisteringUser() =>
            new("User.ErrorWhileRegisteringUser", "An error occurred while registering the user.");
        
        public static Error UserNotFound() =>
            new("User.UserNotFound", "The user was not found.");
        
        public static Error UserNotAuthenticated() =>
            new("User.UserNotAuthenticated", "The user is not authenticated.");
    }
    
    public static class Category
    {
        public static Error CategoryNotFound() =>
            new("Category.CategoryNotFound", "The category was not found.");
        
        public static Error CategoryAccessDenied() =>
            new("Category.CategoryAccessDenied", "You do not have permission to access this category.");
    }
}