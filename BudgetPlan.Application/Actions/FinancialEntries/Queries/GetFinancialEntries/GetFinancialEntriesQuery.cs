using BudgetPlan.Domain.Common.Abstractions.Messaging;
using BudgetPlan.Domain.Enums;

namespace BudgetPlan.Application.Actions.FinancialEntries.Queries.GetFinancialEntries;

public sealed class GetFinancialEntriesQuery : IQuery<GetFinancialEntriesResponse>
{
    public DateOnly? OccurredFrom { get; set; }
    public DateOnly? OccurredTo { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? SubcategoryId { get; set; }
    public CategoryType? Type { get; set; }
}
