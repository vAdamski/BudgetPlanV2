using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Application.Common.Interfaces.Persistence.Marten.Repositories;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.FinancialEntries.Queries.GetFinancialEntries;

public sealed class GetFinancialEntriesQueryHandler(
    IFinancialEntryReadRepository financialEntryReadRepository,
    ICurrentUserService currentUserService)
    : IQueryHandler<GetFinancialEntriesQuery, GetFinancialEntriesResult>
{
    public async Task<Result<GetFinancialEntriesResult>> Handle(
        GetFinancialEntriesQuery request,
        CancellationToken cancellationToken)
    {
        var financialEntries = await financialEntryReadRepository.GetForUserAsync(
            currentUserService.UserId,
            request.OccurredFrom,
            request.OccurredTo,
            request.CategoryId,
            request.SubcategoryId,
            request.Type,
            cancellationToken);

        var dtos = financialEntries
            .Select(x => new FinancialEntryDto(
                x.Id,
                x.CategoryId,
                x.SubcategoryId,
                x.Type,
                x.Amount,
                x.OccurredOn,
                x.IsDeleted))
            .ToList();

        return Result.Success(new GetFinancialEntriesResult(dtos));
    }
}
