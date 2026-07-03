using BudgetPlan.Application.Actions.Categories.Commands.RenameCategory;
using BudgetPlan.Contracts.ControllerContracts.Category.RenameCategory;

namespace BudgetPlan.Api.Common.ContractMappers.Category;

internal static class RenameCategoryMapping
{
    public static RenameCategoryCommand ToCommand(this RenameCategoryRequest request, Guid id)
    {
        return new RenameCategoryCommand(id, request.Name);
    }
}
