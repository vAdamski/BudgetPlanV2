using BudgetPlan.Application.Common.Interfaces.Persistence;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.HealthActions.Queries.CheckHealth;

public sealed class CheckHealthQueryHandler(IDatabaseHealthCheck databaseHealthCheck)
	: IQueryHandler<CheckHealthQuery, CheckHealthResult>
{
	public async Task<Result<CheckHealthResult>> Handle(CheckHealthQuery request, CancellationToken cancellationToken)
	{
		var result = new CheckHealthResult(
			true,
			await databaseHealthCheck.CanConnectAsync(cancellationToken));

		return Result.Success(result);
	}
}
