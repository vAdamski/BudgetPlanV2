using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Application.Common.Interfaces.Persistence.Marten;
using BudgetPlan.Domain.Aggregates.FinancialEntry;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;
using BudgetPlan.Domain.Errors;

namespace BudgetPlan.Application.Actions.FinancialEntries.Commands.DeleteFinancialEntry;

public sealed class DeleteFinancialEntryCommandHandler(
    ICurrentUserService currentUserService,
    IAggregateRepository aggregateRepository)
    : ICommandHandler<DeleteFinancialEntryCommand>
{
    public async Task<Result> Handle(DeleteFinancialEntryCommand request, CancellationToken cancellationToken)
    {
        var financialEntry = await aggregateRepository.TryLoadAsync<FinancialEntry>(
            request.Id,
            null,
            cancellationToken);

        if (financialEntry is null)
            return Result.Failure(ApplicationErrors.FinancialEntry.FinancialEntryNotFound());

        if (financialEntry.UserId != currentUserService.UserId)
            return Result.Failure(ApplicationErrors.FinancialEntry.FinancialEntryAccessDenied());

        var deleteResult = financialEntry.Delete();

        if (deleteResult.IsFailure)
            return deleteResult;

        await aggregateRepository.StoreAsync(financialEntry, cancellationToken);

        return Result.Success();
    }
}
