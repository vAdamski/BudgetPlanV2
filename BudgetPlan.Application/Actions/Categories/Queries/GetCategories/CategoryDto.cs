namespace BudgetPlan.Application.Actions.Categories.Queries.GetCategories;

public record CategoryDto(Guid Id, string Name, List<SubcategoryDto> Subcategories);