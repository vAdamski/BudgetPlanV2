using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.UserAccountManager.Register;

public record RegisterUserCommand(string Email, string Password, string DisplayName) : ICommand<RegisterUserResult>;