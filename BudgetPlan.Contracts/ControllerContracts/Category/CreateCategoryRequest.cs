using BudgetPlan.Contracts.Enums;

namespace BudgetPlan.Contracts.ControllerContracts.Category;

public record CreateCategoryRequest(string Name, CategoryType Type);
public record CreateCategoryResponse(Guid Id);