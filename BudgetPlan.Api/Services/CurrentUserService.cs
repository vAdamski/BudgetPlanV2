using System.Security.Claims;
using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Persistence.Factories;

namespace BudgetPlan.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    public Guid UserId { get; }
    public string Email { get; }
    public string DisplayName { get; }
    public bool IsAuthenticated { get; }

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var context = httpContextAccessor.HttpContext?.User;

        UserId = Guid.Parse(context?.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.Empty.ToString());
        Email = context?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
        DisplayName = context?.FindFirstValue(CustomClaimTypes.DisplayName) ?? string.Empty;

        IsAuthenticated = UserId != Guid.Empty && !string.IsNullOrEmpty(Email);
    }
}