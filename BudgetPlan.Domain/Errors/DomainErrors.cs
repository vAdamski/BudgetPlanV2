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
}
