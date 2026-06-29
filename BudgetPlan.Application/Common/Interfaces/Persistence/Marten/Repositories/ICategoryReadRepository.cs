using CategoryListItem = BudgetPlan.Application.Common.Dtos.Category.CategoryListItem;

namespace BudgetPlan.Application.Common.Interfaces.Persistence.Marten.Repositories;

public interface ICategoryReadRepository
{
    Task<IReadOnlyList<CategoryListItem>> GetForUserAsync(Guid userId, CancellationToken cancellationToken);
}