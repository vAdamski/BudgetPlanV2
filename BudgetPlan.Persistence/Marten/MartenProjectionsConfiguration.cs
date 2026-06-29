using BudgetPlan.Persistence.Marten.Projections.CategoryList;
using JasperFx.Events.Projections;
using Marten;

namespace BudgetPlan.Persistence.Marten;

public static class MartenProjectionsConfiguration
{
    public static StoreOptions ConfigureProjections(this StoreOptions options)
    {
        options.Projections.Add(new UserCategoriesReadModelProjection(), ProjectionLifecycle.Async);
        
        return options;
    }
}