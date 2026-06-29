using BudgetPlan.Application.Common.Dtos.SettlementPeriod;

namespace BudgetPlan.Application.Common.Interfaces.Persistence.Marten.Repositories;

public interface ISettlementPeriodReadRepository
{
    Task<IReadOnlyList<SettlementPeriodListItem>> GetForUserAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task<SettlementPeriodListItem?> GetByIdAsync(
        Guid userId,
        Guid settlementPeriodId,
        CancellationToken cancellationToken);
}
