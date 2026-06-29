namespace BudgetPlan.Persistence.Marten.Projections.CategoryList;

public class SubcategoryListItem
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsArchived { get; set; }
    public DateTime? ArchivedAt { get; set; }   
}