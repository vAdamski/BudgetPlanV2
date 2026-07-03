using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Application.Common.Interfaces.Persistence.Marten;
using BudgetPlan.Domain.Aggregates.Category;
using BudgetPlan.Domain.Aggregates.FinancialEntry;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;
using BudgetPlan.Domain.Errors;

namespace BudgetPlan.Application.Actions.FinancialEntries.Commands.CreateFinancialEntry;

public sealed class CreateFinancialEntryCommandHandler(
    ICurrentUserService currentUserService,
    IAggregateRepository aggregateRepository)
    : ICommandHandler<CreateFinancialEntryCommand, CreateFinancialEntryResult>
{
    public async Task<Result<CreateFinancialEntryResult>> Handle(
        CreateFinancialEntryCommand request,
        CancellationToken cancellationToken)
    {
        var categoryResult = await GetActiveCategoryAsync(
            request.CategoryId,
            request.SubcategoryId,
            cancellationToken);

        if (categoryResult.IsFailure)
            return Result.Failure<CreateFinancialEntryResult>(categoryResult.Error);

        var category = categoryResult.Value;

        var financialEntry = FinancialEntry.Create(
            currentUserService.UserId,
            category.Id,
            request.SubcategoryId,
            category.Type,
            request.Amount,
            request.OccurredOn);

        if (financialEntry.IsFailure)
            return Result.Failure<CreateFinancialEntryResult>(financialEntry.Error);

        await aggregateRepository.StoreAsync(financialEntry.Value, cancellationToken);

        return Result.Success(new CreateFinancialEntryResult(financialEntry.Value.Id));
    }

    private async Task<Result<Category>> GetActiveCategoryAsync(
        Guid categoryId,
        Guid subcategoryId,
        CancellationToken cancellationToken)
    {
        var category = await aggregateRepository.TryLoadAsync<Category>(
            categoryId,
            null,
            cancellationToken);

        if (category is null)
            return Result.Failure<Category>(ApplicationErrors.Category.CategoryNotFound());

        if (category.UserId != currentUserService.UserId)
            return Result.Failure<Category>(ApplicationErrors.Category.CategoryAccessDenied());

        if (category.IsArchived)
            return Result.Failure<Category>(
                DomainErrors.General.InvalidOperation("Cannot create a financial entry for an archived category."));

        var subcategory = category.Subcategories.FirstOrDefault(x => x.Id == subcategoryId);

        if (subcategory is null)
            return Result.Failure<Category>(DomainErrors.General.NotFound("Subcategory", subcategoryId));

        if (subcategory.IsArchived)
            return Result.Failure<Category>(
                DomainErrors.General.InvalidOperation("Cannot create a financial entry for an archived subcategory."));

        return Result.Success(category);
    }
}
