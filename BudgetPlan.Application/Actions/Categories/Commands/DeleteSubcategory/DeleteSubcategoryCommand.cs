using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.Categories.Commands.DeleteSubcategory;

public class DeleteSubcategoryCommand : ICommand
{
    public Guid CategoryId { get; set; }
    public Guid SubcategoryId { get; set; }
}
