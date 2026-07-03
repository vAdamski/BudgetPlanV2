using BudgetPlan.Application.Actions.UserAccountManager.Register;
using BudgetPlan.Contracts.ControllerContracts.Auth;

namespace BudgetPlan.Api.Common.ContractMappers.Auth;

internal static class RegisterUserMappings
{
    public static RegisterUserCommand ToCommand(
        this RegisterUserRequest request)
    {
        return new RegisterUserCommand(
            request.Email,
            request.Password,
            request.DisplayName);
    }

    public static RegisterUserResponse ToResponse(this RegisterUserResult result)
    {
        return new RegisterUserResponse(result.UserId);
    }
}