using BudgetPlan.Application.Common.Interfaces.Persistence.Marten.Repositories;
using BudgetPlan.Persistence.Marten.Projections.SettlementPeriods;
using Marten;
using SettlementPeriodListItem = BudgetPlan.Application.Common.Dtos.SettlementPeriod.SettlementPeriodListItem;

namespace BudgetPlan.Persistence.Marten.Repositories;

public sealed class SettlementPeriodReadRepository(IDocumentStore store) : ISettlementPeriodReadRepository
{
    public async Task<IReadOnlyList<SettlementPeriodListItem>> GetForUserAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        await using var session = store.QuerySession();
        var readModel = await session.Query<UserSettlementPeriodsReadModel>()
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        if (readModel is null)
            return [];

        return readModel.SettlementPeriods
            .OrderBy(x => x.StartDate)
            .ThenBy(x => x.EndDate)
            .Select(ToListItem)
            .ToList();
    }

    public async Task<SettlementPeriodListItem?> GetByIdAsync(
        Guid userId,
        Guid settlementPeriodId,
        CancellationToken cancellationToken)
    {
        await using var session = store.QuerySession();
        var readModel = await session.Query<UserSettlementPeriodsReadModel>()
            .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

        var settlementPeriod = readModel?.SettlementPeriods
            .FirstOrDefault(x => x.Id == settlementPeriodId);

        return settlementPeriod is null ? null : ToListItem(settlementPeriod);
    }

    private static SettlementPeriodListItem ToListItem(SettlementPeriodReadModelItem item)
    {
        return new SettlementPeriodListItem
        {
            Id = item.Id,
            Name = item.Name,
            Currency = item.Currency,
            StartDate = item.StartDate,
            EndDate = item.EndDate
        };
    }
}
