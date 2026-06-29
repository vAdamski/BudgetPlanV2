using BudgetPlan.Domain.Common.Abstractions.Messaging;
using BudgetPlan.Domain.Enums;

namespace BudgetPlan.Application.Actions.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : ICommand<Guid>
{
    public string Name { get; set; } = string.Empty;
    public CategoryType Type { get; set; }
}