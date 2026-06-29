using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Application.Common.Interfaces.Persistence.Marten;
using BudgetPlan.Domain.Aggregates.Category;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;
using BudgetPlan.Domain.Errors;

namespace BudgetPlan.Application.Actions.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler(
    ICurrentUserService currentUserService,
    IAggregateRepository aggregateRepository)
    : ICommandHandler<DeleteCategoryCommand>
{
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await aggregateRepository.TryLoadAsync<Category>(request.Id, null, cancellationToken);

        if (category == null)
            return Result.Failure(ApplicationErrors.Category.CategoryNotFound());

        if (category.UserId != currentUserService.UserId)
            return Result.Failure(ApplicationErrors.Category.CategoryAccessDenied());

        var deleteResult = category.Archive();

        if (deleteResult.IsFailure)
            return Result.Failure(deleteResult.Error);

        await aggregateRepository.StoreAsync(category, cancellationToken);

        return Result.Success();
    }
}
