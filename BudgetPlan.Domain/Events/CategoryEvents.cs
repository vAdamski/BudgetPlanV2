using BudgetPlan.Domain.Enums;

namespace BudgetPlan.Domain.Events;

public static class CategoryEvents
{
    public sealed record CategoryCreated(
        Guid CategoryId,
        Guid UserId,
        string Name,
        CategoryType Type);

    public sealed record CategoryRenamed(
        Guid CategoryId,
        Guid UserId,
        string Name);

    public sealed record SubcategoryAdded(
        Guid CategoryId,
        Guid UserId,
        Guid SubcategoryId,
        string Name);

    public sealed record SubcategoryRenamed(
        Guid CategoryId,
        Guid UserId,
        Guid SubcategoryId,
        string Name);

    public sealed record SubcategoryArchived(
        Guid CategoryId,
        Guid UserId,
        Guid SubcategoryId,
        DateTime ArchivedAt);

    public sealed record CategoryArchived(
        Guid CategoryId,
        Guid UserId,
        DateTime ArchivedAt);
}