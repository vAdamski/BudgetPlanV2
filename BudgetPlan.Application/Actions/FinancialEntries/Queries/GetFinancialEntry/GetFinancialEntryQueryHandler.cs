using BudgetPlan.Application.Actions.FinancialEntries.Queries.GetFinancialEntries;
using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Application.Common.Interfaces.Persistence.Marten.Repositories;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;
using BudgetPlan.Domain.Errors;

namespace BudgetPlan.Application.Actions.FinancialEntries.Queries.GetFinancialEntry;

public sealed class GetFinancialEntryQueryHandler(
    IFinancialEntryReadRepository financialEntryReadRepository,
    ICurrentUserService currentUserService)
    : IQueryHandler<GetFinancialEntryQuery, FinancialEntryDto>
{
    public async Task<Result<FinancialEntryDto>> Handle(
        GetFinancialEntryQuery request,
        CancellationToken cancellationToken)
    {
        var financialEntry = await financialEntryReadRepository.GetByIdAsync(
            currentUserService.UserId,
            request.Id,
            cancellationToken);

        if (financialEntry is null)
            return Result.Failure<FinancialEntryDto>(
                ApplicationErrors.FinancialEntry.FinancialEntryNotFound());

        return Result.Success(new FinancialEntryDto(
            financialEntry.Id,
            financialEntry.CategoryId,
            financialEntry.SubcategoryId,
            financialEntry.Type,
            financialEntry.Amount,
            financialEntry.OccurredOn,
            financialEntry.IsDeleted));
    }
}
