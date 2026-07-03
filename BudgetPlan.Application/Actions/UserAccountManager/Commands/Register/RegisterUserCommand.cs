using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.UserAccountManager.Commands.Register;

public record RegisterUserCommand(string Email, string Password, string DisplayName) : ICommand<RegisterUserResult>;