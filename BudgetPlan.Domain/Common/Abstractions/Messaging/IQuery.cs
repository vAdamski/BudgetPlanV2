using MediatR;

namespace BudgetPlan.Domain.Common.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
