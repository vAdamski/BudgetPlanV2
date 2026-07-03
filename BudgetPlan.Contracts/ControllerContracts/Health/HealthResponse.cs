namespace BudgetPlan.Contracts.ControllerContracts.Health;

public sealed record HealthResponse(bool ApiStatus, bool DatabaseStatus);
