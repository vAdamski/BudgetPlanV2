using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.Categories.Commands.DeleteSubcategory;

public sealed record DeleteSubcategoryCommand(Guid CategoryId, Guid SubcategoryId) : ICommand;
