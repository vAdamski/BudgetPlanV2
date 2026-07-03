using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.Categories.Commands.CreateSubcategory;

public sealed record CreateSubcategoryCommand(Guid CategoryId, string Name) : ICommand<CreateSubcategoryResult>;
