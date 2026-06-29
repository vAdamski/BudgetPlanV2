using BudgetPlan.Domain.Events;
using Marten.Events.Projections;

namespace BudgetPlan.Persistence.Marten.Projections.CategoryList;

public sealed partial class UserCategoriesReadModelProjection
    : MultiStreamProjection<UserCategoriesReadModel, Guid>
{
    public UserCategoriesReadModelProjection()
    {
        Identity<IUserId>(x => x.UserId);
    }

    public UserCategoriesReadModel Create(
        UserAccountEvents.UserAccountCreated @event)
    {
        return new UserCategoriesReadModel
        {
            Id = @event.UserId,
            Categories = []
        };
    }

    public void Apply(
        CategoryEvents.CategoryCreated @event,
        UserCategoriesReadModel model)
    {
        model.Categories.Add(new CategoryListItem
        {
            Id = @event.CategoryId,
            Name = @event.Name,
            Type = @event.Type,
            IsArchived = false,
            ArchivedAt = null,
            Subcategories = []
        });
    }

    public void Apply(
        CategoryEvents.CategoryRenamed @event,
        UserCategoriesReadModel model)
    {
        var category = model.Categories
            .FirstOrDefault(x => x.Id == @event.CategoryId);

        if (category is null)
            return;

        category.Name = @event.Name;
    }

    public void Apply(
        CategoryEvents.CategoryArchived @event,
        UserCategoriesReadModel model)
    {
        var category = model.Categories
            .FirstOrDefault(x => x.Id == @event.CategoryId);

        if (category is null)
            return;

        category.IsArchived = true;
        category.ArchivedAt = @event.ArchivedAt;
    }

    public void Apply(
        CategoryEvents.SubcategoryAdded @event,
        UserCategoriesReadModel model)
    {
        var category = model.Categories
            .FirstOrDefault(x => x.Id == @event.CategoryId);

        if (category is null)
            return;

        category.Subcategories.Add(new SubcategoryListItem
        {
            Id = @event.SubcategoryId,
            Name = @event.Name,
            IsArchived = false,
            ArchivedAt = null
        });
    }

    public void Apply(
        CategoryEvents.SubcategoryRenamed @event,
        UserCategoriesReadModel model)
    {
        var subcategory = model.Categories
            .FirstOrDefault(x => x.Id == @event.CategoryId)?
            .Subcategories
            .FirstOrDefault(x => x.Id == @event.SubcategoryId);

        if (subcategory is null)
            return;

        subcategory.Name = @event.Name;
    }

    public void Apply(
        CategoryEvents.SubcategoryArchived @event,
        UserCategoriesReadModel model)
    {
        var subcategory = model.Categories
            .FirstOrDefault(x => x.Id == @event.CategoryId)?
            .Subcategories
            .FirstOrDefault(x => x.Id == @event.SubcategoryId);

        if (subcategory is null)
            return;

        subcategory.IsArchived = true;
        subcategory.ArchivedAt = @event.ArchivedAt;
    }
}