using BudgetPlan.Application.Common.Interfaces.Persistence.Marten;
using BudgetPlan.Domain.Aggregates.UserAccount;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;
using BudgetPlan.Domain.Entities;
using BudgetPlan.Domain.Errors;
using Microsoft.AspNetCore.Identity;

namespace BudgetPlan.Application.Actions.UserAccountManager.Register;

public sealed class RegisterUserCommandHandler(
    RoleManager<IdentityRole<Guid>> roleManager,
    UserManager<ApplicationUser> userManager,
    IAggregateRepository aggregateRepository)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userRole = await roleManager.CreateAsync(new IdentityRole<Guid>
        {
            Name = "User",
            NormalizedName = "USER",
            ConcurrencyStamp = Guid.NewGuid().ToString()
        });
        
        if (!userRole.Succeeded)
        {
            return Result.Failure<Guid>(ApplicationErrors.User.ErrorWhileRegisteringUser());
        }
        
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            DisplayName = request.DisplayName,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, request.Password);
        
        if (!result.Succeeded)
        {
            return Result.Failure<Guid>(ApplicationErrors.User.ErrorWhileRegisteringUser());
        }
        
        if (!await userManager.IsInRoleAsync(user, "User"))
        {
            var addToRoleResult = await userManager.AddToRoleAsync(user, "User");
            if (!addToRoleResult.Succeeded)
            {
                return Result.Failure<Guid>(ApplicationErrors.User.ErrorWhileRegisteringUser());
            }
        }

        var userAccount = UserAccount.Create(user.Id, user.Email, user.DisplayName);

        await aggregateRepository.StoreAsync(userAccount, cancellationToken);

        return Result.Success(user.Id);
    }
}