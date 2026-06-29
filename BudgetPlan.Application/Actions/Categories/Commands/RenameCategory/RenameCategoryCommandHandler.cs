using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Application.Common.Interfaces.Persistence.Marten;
using BudgetPlan.Domain.Aggregates.Category;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;
using BudgetPlan.Domain.Errors;

namespace BudgetPlan.Application.Actions.Categories.Commands.RenameCategory;

public class RenameCategoryCommandHandler(
    ICurrentUserService currentUserService,
    IAggregateRepository aggregateRepository) : ICommandHandler<RenameCategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RenameCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await aggregateRepository.LoadAsync<Category>(request.Id, null, cancellationToken);

        if (category == null)
            return Result.Failure<Guid>(ApplicationErrors.Category.CategoryNotFound());

        if (category.UserId != currentUserService.UserId)
            return Result.Failure<Guid>(ApplicationErrors.Category.CategoryAccessDenied());

        var renameResult = category.Rename(request.Name);

        if (renameResult.IsFailure)
            return Result.Failure<Guid>(renameResult.Error);

        await aggregateRepository.StoreAsync(category, cancellationToken);

        return Result.Success(category.Id);
    }
}