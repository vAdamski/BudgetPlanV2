using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;
using BudgetPlan.Domain.Errors;

namespace BudgetPlan.Application.Actions.UserAccountManager.Queries.GetCurentUser;

public class GetCurrentUserQueryHandler(ICurrentUserService currentUserService) : IQueryHandler<GetCurrentUserQuery, GetCurrentUserResult>
{
    public async Task<Result<GetCurrentUserResult>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        if (!currentUserService.IsAuthenticated)
            return Result.Failure<GetCurrentUserResult>(ApplicationErrors.User.UserNotAuthenticated());

        var result = new GetCurrentUserResult(
            currentUserService.UserId,
            currentUserService.Email,
            currentUserService.DisplayName,
            currentUserService.Roles);

        return Result.Success(result);
    }
}