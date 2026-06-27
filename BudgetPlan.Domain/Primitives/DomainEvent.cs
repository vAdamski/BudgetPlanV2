using MediatR;

namespace BudgetPlan.Domain.Primitives;

public record DomainEvent(Guid Id) : INotification;
