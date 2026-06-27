using BudgetPlan.Domain.Common;
using MediatR;

namespace BudgetPlan.Domain.Common.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
