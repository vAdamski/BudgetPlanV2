using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommand : ICommand
{
    public Guid Id { get; set; }
}