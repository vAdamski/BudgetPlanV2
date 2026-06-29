using BudgetPlan.Domain.Enums;

namespace BudgetPlan.Domain.Events;

public static class CategoryEvents
{
    public sealed record CategoryCreated(
        Guid CategoryId,
        Guid UserId,
        string Name,
        CategoryType Type) : IUserId;

    public sealed record CategoryRenamed(
        Guid CategoryId,
        Guid UserId,
        string Name) : IUserId;

    public sealed record SubcategoryAdded(
        Guid CategoryId,
        Guid UserId,
        Guid SubcategoryId,
        string Name) : IUserId;

    public sealed record SubcategoryRenamed(
        Guid CategoryId,
        Guid UserId,
        Guid SubcategoryId,
        string Name) : IUserId;

    public sealed record SubcategoryArchived(
        Guid CategoryId,
        Guid UserId,
        Guid SubcategoryId,
        DateTime ArchivedAt) : IUserId;

    public sealed record CategoryArchived(
        Guid CategoryId,
        Guid UserId,
        DateTime ArchivedAt) : IUserId;
}