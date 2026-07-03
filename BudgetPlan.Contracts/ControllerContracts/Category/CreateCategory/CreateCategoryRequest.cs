using BudgetPlan.Contracts.Enums;

namespace BudgetPlan.Contracts.ControllerContracts.Category.CreateCategory;

public record CreateCategoryRequest(string Name, CategoryType Type);