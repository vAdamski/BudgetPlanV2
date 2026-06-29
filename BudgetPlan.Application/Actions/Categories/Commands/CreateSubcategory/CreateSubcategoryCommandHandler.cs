using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Application.Common.Interfaces.Persistence.Marten;
using BudgetPlan.Domain.Aggregates.Category;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;
using BudgetPlan.Domain.Errors;

namespace BudgetPlan.Application.Actions.Categories.Commands.CreateSubcategory;

public class CreateSubcategoryCommandHandler(
    ICurrentUserService currentUserService,
    IAggregateRepository aggregateRepository) : ICommandHandler<CreateSubcategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateSubcategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await aggregateRepository.LoadAsync<Category>(request.CategoryId, null, cancellationToken);

        if (category == null)
            return Result.Failure<Guid>(ApplicationErrors.Category.CategoryNotFound());

        if (category.UserId != currentUserService.UserId)
            return Result.Failure<Guid>(ApplicationErrors.Category.CategoryAccessDenied());

        var createResult = category.AddSubcategory(request.Name);

        if (createResult.IsFailure)
            return Result.Failure<Guid>(createResult.Error);

        await aggregateRepository.StoreAsync(category, cancellationToken);

        return Result.Success(createResult.Value);
    }
}