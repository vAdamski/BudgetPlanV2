using BudgetPlan.Domain.Common.Abstractions.Messaging;
using BudgetPlan.Domain.Enums;

namespace BudgetPlan.Application.Actions.FinancialEntries.Queries.GetFinancialEntries;

public sealed record GetFinancialEntriesQuery(
    DateOnly? OccurredFrom,
    DateOnly? OccurredTo,
    Guid? CategoryId,
    Guid? SubcategoryId,
    CategoryType? Type) : IQuery<GetFinancialEntriesResult>;
