using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.Categories.Queries.GetCategories;

public sealed record GetCategoriesQuery : IQuery<GetCategoriesResult>;
