using BudgetPlan.Domain.Common;

namespace BudgetPlan.Domain.Errors;

public static class DomainErrors
{
	public static class General
	{
		public static Error NotFound(string resourceName, object id) =>
			new("General.NotFound", $"{resourceName} with identifier '{id}' was not found.");

		public static Error InvalidOperation(string message) =>
			new("General.InvalidOperation", message);
	}
	
	public static class Category
	{
		public static Error InvalidUserId => new("Category.InvalidUserId", "The user ID is invalid.");
		public static Error InvalidName => new("Category.InvalidName", "The category name is invalid.");
		public static Error AlreadyExist => new("Category.AlreadyExist", "The category already exist.");
	}

	public static class SettlementPeriod
	{
		public static Error InvalidUserId => new("SettlementPeriod.InvalidUserId", "The user ID is invalid.");
		public static Error InvalidName => new("SettlementPeriod.InvalidName", "The settlement period name is invalid.");
		public static Error InvalidCurrency => new("SettlementPeriod.InvalidCurrency", "The settlement period currency is invalid.");
		public static Error InvalidDateRange => new("SettlementPeriod.InvalidDateRange", "The settlement period end date cannot be earlier than the start date.");
	}

	public static class FinancialEntry
	{
		public static Error InvalidUserId => new("FinancialEntry.InvalidUserId", "The user ID is invalid.");
		public static Error InvalidCategoryId => new("FinancialEntry.InvalidCategoryId", "The category ID is invalid.");
		public static Error InvalidSubcategoryId => new("FinancialEntry.InvalidSubcategoryId", "The subcategory ID is invalid.");
		public static Error InvalidType => new("FinancialEntry.InvalidType", "The financial entry type is invalid.");
		public static Error InvalidAmount => new("FinancialEntry.InvalidAmount", "The financial entry amount must be greater than zero.");
		public static Error AlreadyDeleted => new("FinancialEntry.AlreadyDeleted", "The financial entry has already been deleted.");
	}
}
