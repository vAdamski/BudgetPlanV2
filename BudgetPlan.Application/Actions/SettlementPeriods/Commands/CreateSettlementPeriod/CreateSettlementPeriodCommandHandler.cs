using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Application.Common.Interfaces.Persistence.Marten;
using BudgetPlan.Domain.Aggregates.SettlementPeriod;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.SettlementPeriods.Commands.CreateSettlementPeriod;

public sealed class CreateSettlementPeriodCommandHandler(
    ICurrentUserService currentUserService,
    IAggregateRepository aggregateRepository)
    : ICommandHandler<CreateSettlementPeriodCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateSettlementPeriodCommand request, CancellationToken cancellationToken)
    {
        var settlementPeriod = SettlementPeriod.Create(
            currentUserService.UserId,
            request.Name,
            request.Currency,
            request.StartDate,
            request.EndDate);

        if (settlementPeriod.IsFailure)
            return Result.Failure<Guid>(settlementPeriod.Error);

        await aggregateRepository.StoreAsync(settlementPeriod.Value, cancellationToken);

        return Result.Success(settlementPeriod.Value.Id);
    }
}
