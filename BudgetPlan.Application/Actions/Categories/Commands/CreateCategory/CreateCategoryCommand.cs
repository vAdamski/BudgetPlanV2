using BudgetPlan.Domain.Common.Abstractions.Messaging;
using BudgetPlan.Domain.Enums;

namespace BudgetPlan.Application.Actions.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name, CategoryType Type) : ICommand<CreateCategoryResult>;