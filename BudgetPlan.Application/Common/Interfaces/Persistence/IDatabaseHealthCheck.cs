namespace BudgetPlan.Application.Common.Interfaces.Persistence;

public interface IDatabaseHealthCheck
{
	Task<bool> CanConnectAsync(CancellationToken cancellationToken = default);
}
