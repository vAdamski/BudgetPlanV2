using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.Categories.Commands.RenameCategory;

public sealed record RenameCategoryCommand(Guid Id, string Name) : ICommand<RenameCategoryResult>;
