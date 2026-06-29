using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.Categories.Commands.CreateSubcategory;

public class CreateSubcategoryCommand : ICommand<Guid>
{
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
}