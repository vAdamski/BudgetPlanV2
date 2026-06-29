using BudgetPlan.Application.Common.Interfaces.Persistence.Marten.Repositories;
using BudgetPlan.Persistence.Marten.Projections.CategoryList;
using Marten;
using CategoryListItem = BudgetPlan.Application.Common.Dtos.Category.CategoryListItem;
using SubcategoryListItem = BudgetPlan.Application.Common.Dtos.Category.SubcategoryListItem;

namespace BudgetPlan.Persistence.Marten.Repositories;

public sealed class CategoryReadRepository(IDocumentStore store) : ICategoryReadRepository
{
    public async Task<IReadOnlyList<CategoryListItem>> GetForUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        await using var session = store.QuerySession();
        var categoryReadModel = await session.Query<UserCategoriesReadModel>()
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        
        if (categoryReadModel == null) return new List<CategoryListItem>();

        return categoryReadModel.Categories
            .Where(c => !c.IsArchived)
            .Select(c => new CategoryListItem
            {
                Id = c.Id,
                Name = c.Name,
                Type = c.Type,
                IsArchived = c.IsArchived,
                Subcategories = c.Subcategories
                    .Where(s => !s.IsArchived)
                    .Select(s => new SubcategoryListItem
                    {
                        Id = s.Id,
                        Name = s.Name,
                        IsArchived = s.IsArchived
                    }).ToList()
            }).ToList();
    }
}