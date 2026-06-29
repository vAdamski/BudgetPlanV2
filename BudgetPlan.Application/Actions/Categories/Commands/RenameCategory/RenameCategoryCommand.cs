using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.Categories.Commands.RenameCategory;

public class RenameCategoryCommand : ICommand<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}