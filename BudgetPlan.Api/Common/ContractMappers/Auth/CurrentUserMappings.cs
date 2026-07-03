using BudgetPlan.Application.Actions.UserAccountManager.Queries.GetCurentUser;
using BudgetPlan.Contracts.ControllerContracts.Auth.CurrentUser;

namespace BudgetPlan.Api.Common.ContractMappers.Auth;

internal static class CurrentUserMappings
{
    public static CurrentUserResponse ToResponse(this GetCurrentUserResult result)
    {
        return new CurrentUserResponse(
            result.Id,
            result.Email,
            result.DisplayName,
            result.Roles);
    }
}