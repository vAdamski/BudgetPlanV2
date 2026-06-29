using BudgetPlan.Application.Actions.FinancialEntries.Queries.GetFinancialEntries;
using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.FinancialEntries.Queries.GetFinancialEntry;

public sealed record GetFinancialEntryQuery(Guid Id) : IQuery<FinancialEntryDto>;
