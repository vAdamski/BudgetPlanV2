using BudgetPlan.Domain.Events;
using Marten.Events.Projections;

namespace BudgetPlan.Persistence.Marten.Projections.FinancialEntries;

public sealed partial class UserFinancialEntriesReadModelProjection
    : MultiStreamProjection<UserFinancialEntriesReadModel, Guid>
{
    public UserFinancialEntriesReadModelProjection()
    {
        Identity<IUserId>(x => x.UserId);
    }

    public UserFinancialEntriesReadModel Create(UserAccountEvents.UserAccountCreated @event)
    {
        return new UserFinancialEntriesReadModel
        {
            Id = @event.UserId,
            FinancialEntries = []
        };
    }

    public UserFinancialEntriesReadModel Create(
        FinancialEntryEvents.FinancialEntryCreated @event)
    {
        return new UserFinancialEntriesReadModel
        {
            Id = @event.UserId,
            FinancialEntries =
            [
                ToReadModelItem(@event)
            ]
        };
    }

    public void Apply(
        FinancialEntryEvents.FinancialEntryCreated @event,
        UserFinancialEntriesReadModel model)
    {
        if (model.FinancialEntries.Any(x => x.Id == @event.FinancialEntryId))
            return;

        model.FinancialEntries.Add(ToReadModelItem(@event));
    }

    public void Apply(
        FinancialEntryEvents.FinancialEntryUpdated @event,
        UserFinancialEntriesReadModel model)
    {
        var financialEntry = model.FinancialEntries
            .FirstOrDefault(x => x.Id == @event.FinancialEntryId);

        if (financialEntry is null)
            return;

        financialEntry.CategoryId = @event.CategoryId;
        financialEntry.SubcategoryId = @event.SubcategoryId;
        financialEntry.Type = @event.Type;
        financialEntry.Amount = @event.Amount;
        financialEntry.OccurredOn = @event.OccurredOn;
    }

    public void Apply(
        FinancialEntryEvents.FinancialEntryDeleted @event,
        UserFinancialEntriesReadModel model)
    {
        var financialEntry = model.FinancialEntries
            .FirstOrDefault(x => x.Id == @event.FinancialEntryId);

        if (financialEntry is null)
            return;

        financialEntry.IsDeleted = true;
        financialEntry.DeletedAt = @event.DeletedAt;
    }

    private static FinancialEntryReadModelItem ToReadModelItem(
        FinancialEntryEvents.FinancialEntryCreated @event)
    {
        return new FinancialEntryReadModelItem
        {
            Id = @event.FinancialEntryId,
            CategoryId = @event.CategoryId,
            SubcategoryId = @event.SubcategoryId,
            Type = @event.Type,
            Amount = @event.Amount,
            OccurredOn = @event.OccurredOn,
            IsDeleted = false,
            DeletedAt = null
        };
    }
}
