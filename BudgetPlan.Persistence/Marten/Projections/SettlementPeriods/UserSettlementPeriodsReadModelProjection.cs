using BudgetPlan.Domain.Events;
using Marten.Events.Projections;

namespace BudgetPlan.Persistence.Marten.Projections.SettlementPeriods;

public sealed partial class UserSettlementPeriodsReadModelProjection
    : MultiStreamProjection<UserSettlementPeriodsReadModel, Guid>
{
    public UserSettlementPeriodsReadModelProjection()
    {
        Identity<IUserId>(x => x.UserId);
    }

    public UserSettlementPeriodsReadModel Create(UserAccountEvents.UserAccountCreated @event)
    {
        return new UserSettlementPeriodsReadModel
        {
            Id = @event.UserId,
            SettlementPeriods = []
        };
    }

    public UserSettlementPeriodsReadModel Create(
        SettlementPeriodEvents.SettlementPeriodCreated @event)
    {
        return new UserSettlementPeriodsReadModel
        {
            Id = @event.UserId,
            SettlementPeriods =
            [
                ToReadModelItem(@event)
            ]
        };
    }

    public void Apply(
        SettlementPeriodEvents.SettlementPeriodCreated @event,
        UserSettlementPeriodsReadModel model)
    {
        if (model.SettlementPeriods.Any(x => x.Id == @event.SettlementPeriodId))
            return;

        model.SettlementPeriods.Add(ToReadModelItem(@event));
    }

    public void Apply(
        SettlementPeriodEvents.SettlementPeriodUpdated @event,
        UserSettlementPeriodsReadModel model)
    {
        var settlementPeriod = model.SettlementPeriods
            .FirstOrDefault(x => x.Id == @event.SettlementPeriodId);

        if (settlementPeriod is null)
            return;

        settlementPeriod.Name = @event.Name;
        settlementPeriod.Currency = @event.Currency;
        settlementPeriod.StartDate = @event.StartDate;
        settlementPeriod.EndDate = @event.EndDate;
    }

    private static SettlementPeriodReadModelItem ToReadModelItem(
        SettlementPeriodEvents.SettlementPeriodCreated @event)
    {
        return new SettlementPeriodReadModelItem
        {
            Id = @event.SettlementPeriodId,
            Name = @event.Name,
            Currency = @event.Currency,
            StartDate = @event.StartDate,
            EndDate = @event.EndDate
        };
    }
}
