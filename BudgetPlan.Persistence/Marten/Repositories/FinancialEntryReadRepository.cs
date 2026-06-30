using BudgetPlan.Application.Common.Interfaces.Persistence.Marten.Repositories;
using BudgetPlan.Domain.Enums;
using BudgetPlan.Persistence.Marten.Projections.FinancialEntries;
using Marten;
using FinancialEntryListItem = BudgetPlan.Application.Common.Dtos.FinancialEntry.FinancialEntryListItem;

namespace BudgetPlan.Persistence.Marten.Repositories;

public sealed class FinancialEntryReadRepository(IDocumentStore store) : IFinancialEntryReadRepository
{
    public async Task<IReadOnlyList<FinancialEntryListItem>> GetForUserAsync(
        Guid userId,
        DateOnly? occurredFrom,
        DateOnly? occurredTo,
        Guid? categoryId,
        Guid? subcategoryId,
        CategoryType? type,
        CancellationToken cancellationToken)
    {
        await using var session = store.QuerySession();
        var financialEntries = await session.Query<UserFinancialEntriesReadModel>()
            .Where(x => x.UserId == userId && !x.IsDeleted)
            .ToListAsync(cancellationToken);

        if (!financialEntries.Any())
            return [];

        var query = financialEntries.AsQueryable();

        if (occurredFrom.HasValue)
            query = query.Where(x => x.OccurredOn >= occurredFrom.Value);

        if (occurredTo.HasValue)
            query = query.Where(x => x.OccurredOn <= occurredTo.Value);

        if (categoryId.HasValue)
            query = query.Where(x => x.CategoryId == categoryId.Value);

        if (subcategoryId.HasValue)
            query = query.Where(x => x.SubcategoryId == subcategoryId.Value);

        if (type.HasValue)
            query = query.Where(x => x.Type == type.Value);

        return query
            .OrderByDescending(x => x.OccurredOn)
            .ThenByDescending(x => x.Id)
            .Select(ToListItem)
            .ToList();
    }

    public async Task<FinancialEntryListItem?> GetByIdAsync(
        Guid userId,
        Guid financialEntryId,
        CancellationToken cancellationToken)
    {
        await using var session = store.QuerySession();
        var financialEntry = await session.Query<UserFinancialEntriesReadModel>()
            .FirstOrDefaultAsync(x => 
                    x.Id == userId &&
                    !x.IsDeleted &&
                    x.Id == financialEntryId
                , cancellationToken);

        return financialEntry is null ? null : ToListItem(financialEntry);
    }

    private static FinancialEntryListItem ToListItem(UserFinancialEntriesReadModel item)
    {
        return new FinancialEntryListItem
        {
            Id = item.Id,
            CategoryId = item.CategoryId,
            SubcategoryId = item.SubcategoryId,
            Type = item.Type,
            Amount = item.Amount,
            OccurredOn = item.OccurredOn,
            IsDeleted = item.IsDeleted
        };
    }
}
