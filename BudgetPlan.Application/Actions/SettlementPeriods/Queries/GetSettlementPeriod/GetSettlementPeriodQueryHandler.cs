using BudgetPlan.Application.Actions.SettlementPeriods.Queries.GetSettlementPeriods;
using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Application.Common.Interfaces.Persistence.Marten.Repositories;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;
using BudgetPlan.Domain.Errors;

namespace BudgetPlan.Application.Actions.SettlementPeriods.Queries.GetSettlementPeriod;

public sealed class GetSettlementPeriodQueryHandler(
    ISettlementPeriodReadRepository settlementPeriodReadRepository,
    ICurrentUserService currentUserService)
    : IQueryHandler<GetSettlementPeriodQuery, SettlementPeriodDto>
{
    public async Task<Result<SettlementPeriodDto>> Handle(
        GetSettlementPeriodQuery request,
        CancellationToken cancellationToken)
    {
        var settlementPeriod = await settlementPeriodReadRepository.GetByIdAsync(
            currentUserService.UserId,
            request.Id,
            cancellationToken);

        if (settlementPeriod is null)
            return Result.Failure<SettlementPeriodDto>(
                ApplicationErrors.SettlementPeriod.SettlementPeriodNotFound());

        return Result.Success(new SettlementPeriodDto(
            settlementPeriod.Id,
            settlementPeriod.Name,
            settlementPeriod.Currency,
            settlementPeriod.StartDate,
            settlementPeriod.EndDate));
    }
}
