using BudgetPlan.Application.Common.Dtos.FinancialEntry;
using BudgetPlan.Domain.Enums;

namespace BudgetPlan.Application.Common.Interfaces.Persistence.Marten.Repositories;

public interface IFinancialEntryReadRepository
{
    Task<IReadOnlyList<FinancialEntryListItem>> GetForUserAsync(
        Guid userId,
        DateOnly? occurredFrom,
        DateOnly? occurredTo,
        Guid? categoryId,
        Guid? subcategoryId,
        CategoryType? type,
        CancellationToken cancellationToken);

    Task<FinancialEntryListItem?> GetByIdAsync(
        Guid userId,
        Guid financialEntryId,
        CancellationToken cancellationToken);
}
