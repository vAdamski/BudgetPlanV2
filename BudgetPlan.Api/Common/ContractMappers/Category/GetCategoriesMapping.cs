using BudgetPlan.Application.Actions.Categories.Queries.GetCategories;
using BudgetPlan.Contracts.ControllerContracts.Category;
using BudgetPlan.Contracts.ControllerContracts.Category.GetCategories;

namespace BudgetPlan.Api.Common.ContractMappers.Category;

internal static class GetCategoriesMapping
{
    public static GetCategoriesResponse ToResponse(this GetCategoriesResult result)
    {
        return new GetCategoriesResponse(
            result.Categories.Select(c => new CategoryResponse(
                c.Id,
                c.Name,
                c.Subcategories.Select(s => new SubcategoryResponse(
                    s.Id,
                    s.Name)).ToList())).ToList());
    }
}