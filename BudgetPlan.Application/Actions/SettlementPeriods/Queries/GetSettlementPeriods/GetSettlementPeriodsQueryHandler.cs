using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Application.Common.Interfaces.Persistence.Marten.Repositories;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.SettlementPeriods.Queries.GetSettlementPeriods;

public sealed class GetSettlementPeriodsQueryHandler(
    ISettlementPeriodReadRepository settlementPeriodReadRepository,
    ICurrentUserService currentUserService)
    : IQueryHandler<GetSettlementPeriodsQuery, GetSettlementPeriodsResult>
{
    public async Task<Result<GetSettlementPeriodsResult>> Handle(
        GetSettlementPeriodsQuery request,
        CancellationToken cancellationToken)
    {
        var settlementPeriods = await settlementPeriodReadRepository.GetForUserAsync(
            currentUserService.UserId,
            cancellationToken);

        var dtos = settlementPeriods
            .Select(x => new SettlementPeriodDto(
                x.Id,
                x.Name,
                x.Currency,
                x.StartDate,
                x.EndDate))
            .ToList();

        return Result.Success(new GetSettlementPeriodsResult(dtos));
    }
}
