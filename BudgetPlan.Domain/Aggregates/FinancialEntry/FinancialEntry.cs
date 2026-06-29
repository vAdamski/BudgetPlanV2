using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Enums;
using BudgetPlan.Domain.Errors;
using BudgetPlan.Domain.Events;

namespace BudgetPlan.Domain.Aggregates.FinancialEntry;

public sealed class FinancialEntry : AggregateBase
{
    public Guid UserId { get; private set; }
    public Guid CategoryId { get; private set; }
    public Guid SubcategoryId { get; private set; }
    public CategoryType Type { get; private set; }
    public decimal Amount { get; private set; }
    public DateOnly OccurredOn { get; private set; }
    public bool IsDeleted { get; private set; }

    public FinancialEntry()
    {
    }

    public static Result<FinancialEntry> Create(
        Guid userId,
        Guid categoryId,
        Guid subcategoryId,
        CategoryType type,
        decimal amount,
        DateOnly occurredOn)
    {
        var validationResult = Validate(userId, categoryId, subcategoryId, type, amount);

        if (validationResult.IsFailure)
            return Result.Failure<FinancialEntry>(validationResult.Error);

        var financialEntry = new FinancialEntry();

        var financialEntryCreated = new FinancialEntryEvents.FinancialEntryCreated(
            Guid.CreateVersion7(),
            userId,
            categoryId,
            subcategoryId,
            type,
            amount,
            occurredOn);

        financialEntry.Apply(financialEntryCreated);
        financialEntry.AddUncommittedEvent(financialEntryCreated);

        return financialEntry;
    }

    public Result Update(
        Guid categoryId,
        Guid subcategoryId,
        CategoryType type,
        decimal amount,
        DateOnly occurredOn)
    {
        if (IsDeleted)
            return Result.Failure(DomainErrors.FinancialEntry.AlreadyDeleted);

        var validationResult = Validate(UserId, categoryId, subcategoryId, type, amount);

        if (validationResult.IsFailure)
            return validationResult;

        if (CategoryId == categoryId &&
            SubcategoryId == subcategoryId &&
            Type == type &&
            Amount == amount &&
            OccurredOn == occurredOn)
            return Result.Success();

        var financialEntryUpdated = new FinancialEntryEvents.FinancialEntryUpdated(
            Id,
            UserId,
            categoryId,
            subcategoryId,
            type,
            amount,
            occurredOn);

        Apply(financialEntryUpdated);
        AddUncommittedEvent(financialEntryUpdated);

        return Result.Success();
    }

    public Result Delete()
    {
        if (IsDeleted)
            return Result.Success();

        var financialEntryDeleted = new FinancialEntryEvents.FinancialEntryDeleted(
            Id,
            UserId,
            DateTime.UtcNow);

        Apply(financialEntryDeleted);
        AddUncommittedEvent(financialEntryDeleted);

        return Result.Success();
    }

    public void Apply(FinancialEntryEvents.FinancialEntryCreated @event)
    {
        Id = @event.FinancialEntryId;
        UserId = @event.UserId;
        CategoryId = @event.CategoryId;
        SubcategoryId = @event.SubcategoryId;
        Type = @event.Type;
        Amount = @event.Amount;
        OccurredOn = @event.OccurredOn;
        IsDeleted = false;

        Version++;
    }

    public void Apply(FinancialEntryEvents.FinancialEntryUpdated @event)
    {
        CategoryId = @event.CategoryId;
        SubcategoryId = @event.SubcategoryId;
        Type = @event.Type;
        Amount = @event.Amount;
        OccurredOn = @event.OccurredOn;

        Version++;
    }

    public void Apply(FinancialEntryEvents.FinancialEntryDeleted @event)
    {
        IsDeleted = true;

        Version++;
    }

    private static Result Validate(
        Guid userId,
        Guid categoryId,
        Guid subcategoryId,
        CategoryType type,
        decimal amount)
    {
        if (userId == Guid.Empty)
            return Result.Failure(DomainErrors.FinancialEntry.InvalidUserId);

        if (categoryId == Guid.Empty)
            return Result.Failure(DomainErrors.FinancialEntry.InvalidCategoryId);

        if (subcategoryId == Guid.Empty)
            return Result.Failure(DomainErrors.FinancialEntry.InvalidSubcategoryId);

        if (!Enum.IsDefined(type))
            return Result.Failure(DomainErrors.FinancialEntry.InvalidType);

        if (amount <= 0)
            return Result.Failure(DomainErrors.FinancialEntry.InvalidAmount);

        return Result.Success();
    }
}
