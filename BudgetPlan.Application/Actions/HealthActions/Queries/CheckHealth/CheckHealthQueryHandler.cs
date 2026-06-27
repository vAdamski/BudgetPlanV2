using BudgetPlan.Application.Common.Interfaces.Persistence;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.HealthActions.Queries.CheckHealth;

public sealed class CheckHealthQueryHandler(IDatabaseHealthCheck databaseHealthCheck)
	: IQueryHandler<CheckHealthQuery, HealthViewModel>
{
	public async Task<Result<HealthViewModel>> Handle(CheckHealthQuery request, CancellationToken cancellationToken)
	{
		var result = new HealthViewModel
		{
			ApiStatus = true,
			DatabaseStatus = await databaseHealthCheck.CanConnectAsync(cancellationToken)
		};

		return Result.Success(result);
	}
}
