using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.Categories.Commands.RenameSubcategory;

public class RenameSubcategoryCommand : ICommand<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}