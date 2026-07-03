using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.Categories.Commands.RenameSubcategory;

public sealed record RenameSubcategoryCommand(Guid CategoryId, Guid SubcategoryId, string Name)
    : ICommand<RenameSubcategoryResult>;
