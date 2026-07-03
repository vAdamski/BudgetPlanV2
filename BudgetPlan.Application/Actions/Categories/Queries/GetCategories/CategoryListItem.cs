namespace BudgetPlan.Application.Actions.Categories.Queries.GetCategories;

public record CategoryListItem(Guid Id, string Name, List<SubcategoryListItem> Subcategories);