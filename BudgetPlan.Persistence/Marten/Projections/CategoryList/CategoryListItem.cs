using BudgetPlan.Domain.Enums;

namespace BudgetPlan.Persistence.Marten.Projections.CategoryList;

public class CategoryListItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public CategoryType Type { get; set; }
    public List<SubcategoryListItem> Subcategories { get; set; } = new();
    public bool IsArchived { get; set; }
    public DateTime? ArchivedAt { get; set; }
}