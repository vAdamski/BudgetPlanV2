using Microsoft.AspNetCore.Identity;

namespace BudgetPlan.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string DisplayName { get; set; } = null!;
}