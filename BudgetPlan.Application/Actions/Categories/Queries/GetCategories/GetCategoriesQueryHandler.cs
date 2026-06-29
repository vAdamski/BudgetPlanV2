using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Application.Common.Interfaces.Persistence.Marten.Repositories;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler(
    ICategoryReadRepository categoryReadRepository,
    ICurrentUserService currentUserService)
    : IQueryHandler<GetCategoriesQuery, GetCategoriesResponse>
{
    public async Task<Result<GetCategoriesResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryReadRepository.GetForUserAsync(currentUserService.UserId, cancellationToken);

        var categoryDtos = categories.Select(c => new CategoryDto(
            c.Id,
            c.Name,
            c.Subcategories.Select(s => new SubcategoryDto(s.Id, s.Name)).ToList()
        )).ToList();

        return Result.Success(new GetCategoriesResponse(categoryDtos));
    }
}