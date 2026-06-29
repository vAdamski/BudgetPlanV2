using BudgetPlan.Domain.Enums;

namespace BudgetPlan.Application.Common.Dtos.Category;

public class CategoryListItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public CategoryType Type { get; set; }
    public bool IsArchived { get; set; }
    public List<SubcategoryListItem> Subcategories { get; set; } = new();
}