using BudgetPlan.Application.Actions.Categories.Commands.RenameSubcategory;
using BudgetPlan.Contracts.ControllerContracts.Category.RenameSubcategory;

namespace BudgetPlan.Api.Common.ContractMappers.Category;

internal static class RenameSubcategoryMapping
{
    public static RenameSubcategoryCommand ToCommand(
        this RenameSubcategoryRequest request,
        Guid categoryId,
        Guid subcategoryId)
    {
        return new RenameSubcategoryCommand(categoryId, subcategoryId, request.Name);
    }
}
