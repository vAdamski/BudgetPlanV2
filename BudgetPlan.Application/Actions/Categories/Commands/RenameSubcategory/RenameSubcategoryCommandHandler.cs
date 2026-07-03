using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Application.Common.Interfaces.Persistence.Marten;
using BudgetPlan.Domain.Aggregates.Category;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;
using BudgetPlan.Domain.Errors;

namespace BudgetPlan.Application.Actions.Categories.Commands.RenameSubcategory;

public class RenameSubcategoryCommandHandler(
    ICurrentUserService currentUserService,
    IAggregateRepository aggregateRepository) : ICommandHandler<RenameSubcategoryCommand, RenameSubcategoryResult>
{
    public async Task<Result<RenameSubcategoryResult>> Handle(
        RenameSubcategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = await aggregateRepository.TryLoadAsync<Category>(request.CategoryId, null, cancellationToken);

        if (category == null)
            return Result.Failure<RenameSubcategoryResult>(ApplicationErrors.Category.CategoryNotFound());

        if (category.UserId != currentUserService.UserId)
            return Result.Failure<RenameSubcategoryResult>(ApplicationErrors.Category.CategoryAccessDenied());

        var renameResult = category.RenameSubcategory(request.SubcategoryId, request.Name);

        if (renameResult.IsFailure)
            return Result.Failure<RenameSubcategoryResult>(renameResult.Error);

        await aggregateRepository.StoreAsync(category, cancellationToken);

        return Result.Success(new RenameSubcategoryResult(request.SubcategoryId));
    }
}
