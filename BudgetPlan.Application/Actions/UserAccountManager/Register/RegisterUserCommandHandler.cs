using BudgetPlan.Application.Common.Interfaces.Persistence.Marten;
using BudgetPlan.Domain.Aggregates.UserAccount;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;
using BudgetPlan.Domain.Entities;
using BudgetPlan.Domain.Errors;
using Microsoft.AspNetCore.Identity;

namespace BudgetPlan.Application.Actions.UserAccountManager.Register;

public sealed class RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, IAggregateRepository aggregateRepository)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            DisplayName = request.DisplayName,
            EmailConfirmed = false
        };

        var result = await userManager.CreateAsync(user, request.Password);

        var userAccount = UserAccount.Create(user.Id, user.Email, user.DisplayName);

        await aggregateRepository.StoreAsync(userAccount, cancellationToken);
        
        if (!result.Succeeded)
        {
            return Result.Failure<Guid>(ApplicationErrors.User.ErrorWhileRegisteringUser());
        }

        return Result.Success(user.Id);
    }
}