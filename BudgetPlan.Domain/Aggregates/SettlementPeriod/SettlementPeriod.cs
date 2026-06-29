using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Errors;
using BudgetPlan.Domain.Events;

namespace BudgetPlan.Domain.Aggregates.SettlementPeriod;

public sealed class SettlementPeriod : AggregateBase
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = null!;
    public string Currency { get; private set; } = null!;
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }

    public SettlementPeriod()
    {
    }

    public static Result<SettlementPeriod> Create(
        Guid userId,
        string name,
        string currency,
        DateOnly startDate,
        DateOnly endDate)
    {
        var validationResult = Validate(userId, name, currency, startDate, endDate);

        if (validationResult.IsFailure)
            return Result.Failure<SettlementPeriod>(validationResult.Error);

        var settlementPeriod = new SettlementPeriod();

        var settlementPeriodCreated = new SettlementPeriodEvents.SettlementPeriodCreated(
            Guid.CreateVersion7(),
            userId,
            name.Trim(),
            currency.Trim().ToUpperInvariant(),
            startDate,
            endDate);

        settlementPeriod.Apply(settlementPeriodCreated);
        settlementPeriod.AddUncommittedEvent(settlementPeriodCreated);

        return settlementPeriod;
    }

    public Result Update(
        string name,
        string currency,
        DateOnly startDate,
        DateOnly endDate)
    {
        var validationResult = Validate(UserId, name, currency, startDate, endDate);

        if (validationResult.IsFailure)
            return validationResult;

        var normalizedName = name.Trim();
        var normalizedCurrency = currency.Trim().ToUpperInvariant();

        if (Name == normalizedName &&
            Currency == normalizedCurrency &&
            StartDate == startDate &&
            EndDate == endDate)
            return Result.Success();

        var settlementPeriodUpdated = new SettlementPeriodEvents.SettlementPeriodUpdated(
            Id,
            UserId,
            normalizedName,
            normalizedCurrency,
            startDate,
            endDate);

        Apply(settlementPeriodUpdated);
        AddUncommittedEvent(settlementPeriodUpdated);

        return Result.Success();
    }

    public void Apply(SettlementPeriodEvents.SettlementPeriodCreated @event)
    {
        Id = @event.SettlementPeriodId;
        UserId = @event.UserId;
        Name = @event.Name;
        Currency = @event.Currency;
        StartDate = @event.StartDate;
        EndDate = @event.EndDate;

        Version++;
    }

    public void Apply(SettlementPeriodEvents.SettlementPeriodUpdated @event)
    {
        Name = @event.Name;
        Currency = @event.Currency;
        StartDate = @event.StartDate;
        EndDate = @event.EndDate;

        Version++;
    }

    private static Result Validate(
        Guid userId,
        string name,
        string currency,
        DateOnly startDate,
        DateOnly endDate)
    {
        if (userId == Guid.Empty)
            return Result.Failure(DomainErrors.SettlementPeriod.InvalidUserId);

        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure(DomainErrors.SettlementPeriod.InvalidName);

        if (string.IsNullOrWhiteSpace(currency))
            return Result.Failure(DomainErrors.SettlementPeriod.InvalidCurrency);

        if (endDate < startDate)
            return Result.Failure(DomainErrors.SettlementPeriod.InvalidDateRange);

        return Result.Success();
    }
}
