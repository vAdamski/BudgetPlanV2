using BudgetPlan.Persistence.Marten.Projections.CategoryList;
using BudgetPlan.Persistence.Marten.Projections.FinancialEntries;
using BudgetPlan.Persistence.Marten.Projections.SettlementPeriods;
using JasperFx.Events.Projections;
using Marten;

namespace BudgetPlan.Persistence.Marten;

public static class MartenProjectionsConfiguration
{
    public static StoreOptions ConfigureProjections(this StoreOptions options)
    {
        options.Projections.Add(new UserCategoriesReadModelProjection(), ProjectionLifecycle.Async);
        options.Projections.Add(new UserSettlementPeriodsReadModelProjection(), ProjectionLifecycle.Async);
        options.Projections.Add(new UserFinancialEntriesReadModelProjection(), ProjectionLifecycle.Async);
        
        return options;
    }
}
