using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Application.Common.Interfaces.Persistence.Marten;
using BudgetPlan.Domain.Aggregates.Category;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;
using BudgetPlan.Domain.Errors;

namespace BudgetPlan.Application.Actions.Categories.Commands.RenameCategory;

public class RenameCategoryCommandHandler(
    ICurrentUserService currentUserService,
    IAggregateRepository aggregateRepository) : ICommandHandler<RenameCategoryCommand, RenameCategoryResult>
{
    public async Task<Result<RenameCategoryResult>> Handle(RenameCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await aggregateRepository.TryLoadAsync<Category>(request.Id, null, cancellationToken);

        if (category == null)
            return Result.Failure<RenameCategoryResult>(ApplicationErrors.Category.CategoryNotFound());

        if (category.UserId != currentUserService.UserId)
            return Result.Failure<RenameCategoryResult>(ApplicationErrors.Category.CategoryAccessDenied());

        var renameResult = category.Rename(request.Name);

        if (renameResult.IsFailure)
            return Result.Failure<RenameCategoryResult>(renameResult.Error);

        await aggregateRepository.StoreAsync(category, cancellationToken);

        return Result.Success(new RenameCategoryResult(category.Id));
    }
}
