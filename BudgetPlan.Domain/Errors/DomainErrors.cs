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
}
