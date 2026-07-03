using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Application.Common.Interfaces.Persistence.Marten.Repositories;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler(
    ICategoryReadRepository categoryReadRepository,
    ICurrentUserService currentUserService)
    : IQueryHandler<GetCategoriesQuery, GetCategoriesResult>
{
    public async Task<Result<GetCategoriesResult>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryReadRepository.GetForUserAsync(currentUserService.UserId, cancellationToken);

        var categoryDtos = categories.Select(c => new CategoryListItem(
            c.Id,
            c.Name,
            c.Subcategories.Select(s => new SubcategoryListItem(s.Id, s.Name)).ToList()
        )).ToList();

        return Result.Success(new GetCategoriesResult(categoryDtos));
    }
}