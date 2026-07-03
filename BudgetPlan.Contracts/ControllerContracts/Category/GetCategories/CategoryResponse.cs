namespace BudgetPlan.Contracts.ControllerContracts.Category.GetCategories;

public sealed record CategoryResponse(Guid Id, string Name, List<SubcategoryResponse> Subcategories);