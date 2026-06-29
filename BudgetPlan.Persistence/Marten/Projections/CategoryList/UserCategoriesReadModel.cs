namespace BudgetPlan.Persistence.Marten.Projections.CategoryList;

public class UserCategoriesReadModel
{
    public Guid Id { get; set; }
    public List<CategoryListItem> Categories { get; set; } = new();
}