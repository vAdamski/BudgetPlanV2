using BudgetPlan.Application.Actions.Categories.Commands.CreateSubcategory;
using BudgetPlan.Contracts.ControllerContracts.Category.CreateSubcategory;

namespace BudgetPlan.Api.Common.ContractMappers.Category;

internal static class CreateSubcategoryMapping
{
    public static CreateSubcategoryCommand ToCommand(this CreateSubcategoryRequest request, Guid categoryId)
    {
        return new CreateSubcategoryCommand(categoryId, request.Name);
    }

    public static CreateSubcategoryResponse ToResponse(this CreateSubcategoryResult result)
    {
        return new CreateSubcategoryResponse(result.Id);
    }
}
