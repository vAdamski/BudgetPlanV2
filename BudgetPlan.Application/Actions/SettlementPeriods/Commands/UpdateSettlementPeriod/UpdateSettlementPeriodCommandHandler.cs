using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Application.Common.Interfaces.Persistence.Marten;
using BudgetPlan.Domain.Aggregates.SettlementPeriod;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;
using BudgetPlan.Domain.Errors;

namespace BudgetPlan.Application.Actions.SettlementPeriods.Commands.UpdateSettlementPeriod;

public sealed class UpdateSettlementPeriodCommandHandler(
    ICurrentUserService currentUserService,
    IAggregateRepository aggregateRepository)
    : ICommandHandler<UpdateSettlementPeriodCommand>
{
    public async Task<Result> Handle(UpdateSettlementPeriodCommand request, CancellationToken cancellationToken)
    {
        var settlementPeriod = await aggregateRepository.TryLoadAsync<SettlementPeriod>(
            request.Id,
            null,
            cancellationToken);

        if (settlementPeriod is null)
            return Result.Failure(ApplicationErrors.SettlementPeriod.SettlementPeriodNotFound());

        if (settlementPeriod.UserId != currentUserService.UserId)
            return Result.Failure(ApplicationErrors.SettlementPeriod.SettlementPeriodAccessDenied());

        var updateResult = settlementPeriod.Update(
            request.Name,
            request.Currency,
            request.StartDate,
            request.EndDate);

        if (updateResult.IsFailure)
            return updateResult;

        await aggregateRepository.StoreAsync(settlementPeriod, cancellationToken);

        return Result.Success();
    }
}
