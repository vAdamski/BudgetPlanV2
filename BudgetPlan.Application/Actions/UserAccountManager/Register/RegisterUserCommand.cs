using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.UserAccountManager.Register;

public class RegisterUserCommand : ICommand<Guid>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string DisplayName { get; set; }
}