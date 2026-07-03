using BudgetPlan.Application.Actions.HealthActions.Queries.CheckHealth;
using BudgetPlan.Contracts.ControllerContracts.Health;

namespace BudgetPlan.Api.Common.ContractMappers.Health;

internal static class HealthMapping
{
    public static HealthResponse ToResponse(this CheckHealthResult result)
    {
        return new HealthResponse(result.ApiStatus, result.DatabaseStatus);
    }
}
