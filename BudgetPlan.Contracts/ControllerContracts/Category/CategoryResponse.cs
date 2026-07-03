namespace BudgetPlan.Contracts.ControllerContracts.Category;

public sealed record CategoryResponse(Guid Id, string Name, List<SubcategoryResponse> Subcategories);