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

    public static class SettlementPeriod
    {
        public static Error SettlementPeriodNotFound() =>
            new("SettlementPeriod.SettlementPeriodNotFound", "The settlement period was not found.");

        public static Error SettlementPeriodAccessDenied() =>
            new("SettlementPeriod.SettlementPeriodAccessDenied", "You do not have permission to access this settlement period.");
    }

    public static class FinancialEntry
    {
        public static Error FinancialEntryNotFound() =>
            new("FinancialEntry.FinancialEntryNotFound", "The financial entry was not found.");

        public static Error FinancialEntryAccessDenied() =>
            new("FinancialEntry.FinancialEntryAccessDenied", "You do not have permission to access this financial entry.");
    }
}
