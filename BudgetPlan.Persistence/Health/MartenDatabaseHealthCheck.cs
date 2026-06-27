using BudgetPlan.Application.Common.Interfaces.Persistence;
using Marten;

namespace BudgetPlan.Persistence.Health;

internal sealed class MartenDatabaseHealthCheck(IDocumentStore documentStore) : IDatabaseHealthCheck
{
	public async Task<bool> CanConnectAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			using var session = documentStore.QuerySession();
			await session.QueryAsync<int>("select 1", cancellationToken);

			return true;
		}
		catch
		{
			return false;
		}
	}
}
