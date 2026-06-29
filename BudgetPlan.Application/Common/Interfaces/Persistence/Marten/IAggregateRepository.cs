using BudgetPlan.Domain.Aggregates;

namespace BudgetPlan.Application.Common.Interfaces.Persistence.Marten;

public interface IAggregateRepository
{
    Task StoreAsync(AggregateBase aggregate, CancellationToken ct = default);

    Task<T> LoadAsync<T>(
        Guid id,
        int? version = null,
        CancellationToken ct = default
    ) where T : AggregateBase;

    Task<T?> TryLoadAsync<T>(
        Guid id,
        int? version = null,
        CancellationToken ct = default
    ) where T : AggregateBase;
}
