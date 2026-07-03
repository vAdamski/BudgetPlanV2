using BudgetPlan.Api.Common.ContractMappers.Enums;
using BudgetPlan.Application.Actions.Categories.Commands.CreateCategory;
using BudgetPlan.Contracts.ControllerContracts.Category.CreateCategory;

namespace BudgetPlan.Api.Common.ContractMappers.Category;

internal static class CreateCategoryMapping
{
    public static CreateCategoryCommand ToCommand(this CreateCategoryRequest request)
    {
        return new CreateCategoryCommand(
            request.Name, 
            request.Type.ToDomain());
    }
    
    public static CreateCategoryResponse ToResponse(this CreateCategoryResult result)
    {
        return new CreateCategoryResponse(result.Id);
    }
}