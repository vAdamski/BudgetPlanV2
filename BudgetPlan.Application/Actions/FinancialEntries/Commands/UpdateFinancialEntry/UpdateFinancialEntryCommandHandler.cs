using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Application.Common.Interfaces.Persistence.Marten;
using BudgetPlan.Domain.Aggregates.Category;
using BudgetPlan.Domain.Aggregates.FinancialEntry;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;
using BudgetPlan.Domain.Errors;

namespace BudgetPlan.Application.Actions.FinancialEntries.Commands.UpdateFinancialEntry;

public sealed class UpdateFinancialEntryCommandHandler(
    ICurrentUserService currentUserService,
    IAggregateRepository aggregateRepository)
    : ICommandHandler<UpdateFinancialEntryCommand>
{
    public async Task<Result> Handle(UpdateFinancialEntryCommand request, CancellationToken cancellationToken)
    {
        var financialEntry = await aggregateRepository.TryLoadAsync<FinancialEntry>(
            request.Id,
            null,
            cancellationToken);

        if (financialEntry is null)
            return Result.Failure(ApplicationErrors.FinancialEntry.FinancialEntryNotFound());

        if (financialEntry.UserId != currentUserService.UserId)
            return Result.Failure(ApplicationErrors.FinancialEntry.FinancialEntryAccessDenied());

        var categoryResult = await GetActiveCategoryAsync(
            request.CategoryId,
            request.SubcategoryId,
            cancellationToken);

        if (categoryResult.IsFailure)
            return Result.Failure(categoryResult.Error);

        var category = categoryResult.Value;

        var updateResult = financialEntry.Update(
            category.Id,
            request.SubcategoryId,
            category.Type,
            request.Amount,
            request.OccurredOn);

        if (updateResult.IsFailure)
            return updateResult;

        await aggregateRepository.StoreAsync(financialEntry, cancellationToken);

        return Result.Success();
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
                DomainErrors.General.InvalidOperation("Cannot update a financial entry with an archived category."));

        var subcategory = category.Subcategories.FirstOrDefault(x => x.Id == subcategoryId);

        if (subcategory is null)
            return Result.Failure<Category>(DomainErrors.General.NotFound("Subcategory", subcategoryId));

        if (subcategory.IsArchived)
            return Result.Failure<Category>(
                DomainErrors.General.InvalidOperation("Cannot update a financial entry with an archived subcategory."));

        return Result.Success(category);
    }
}
