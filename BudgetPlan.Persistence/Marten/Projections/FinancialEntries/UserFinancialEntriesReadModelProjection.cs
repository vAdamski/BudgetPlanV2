using BudgetPlan.Domain.Enums;
using BudgetPlan.Domain.Events;
using Marten.Events.Aggregation;
using Marten.Events.Projections;

namespace BudgetPlan.Persistence.Marten.Projections.FinancialEntries;

public sealed partial class UserFinancialEntriesReadModelProjection
    : SingleStreamProjection<UserFinancialEntriesReadModel, Guid>
{
    public static UserFinancialEntriesReadModel Create(FinancialEntryEvents.FinancialEntryCreated @event)
    {
        UserFinancialEntriesReadModel model = new UserFinancialEntriesReadModel
        {
            Id = @event.FinancialEntryId,
            UserId = @event.UserId,
            CategoryId = @event.CategoryId,
            SubcategoryId = @event.SubcategoryId,
            Type = @event.Type,
            Amount = @event.Amount,
            OccurredOn = @event.OccurredOn,
            IsDeleted = false
        };
        
        return model;
    }
    
    public void Update(FinancialEntryEvents.FinancialEntryUpdated @event,UserFinancialEntriesReadModel model)
    {
        model.Id = @event.FinancialEntryId;
        model.UserId = @event.UserId;
        model.CategoryId = @event.CategoryId;
        model.SubcategoryId = @event.SubcategoryId;
        model.Type = @event.Type;
        model.Amount = @event.Amount;
        model.OccurredOn = @event.OccurredOn;
    }
    
    public void Delete(FinancialEntryEvents.FinancialEntryDeleted @event, UserFinancialEntriesReadModel model)
    {
        model.IsDeleted = true;
        model.DeletedAt = @event.DeletedAt;
    }
}
