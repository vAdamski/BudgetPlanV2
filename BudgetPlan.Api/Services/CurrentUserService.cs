using System.Security.Claims;
using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Domain.Entities;
using BudgetPlan.Persistence.Factories;
using Microsoft.AspNetCore.Identity;

namespace BudgetPlan.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    public Guid UserId { get; }
    public string Email { get; }
    public string DisplayName { get; }
    public IReadOnlyList<string> Roles { get; } = [];
    public bool IsAuthenticated { get; }

    public CurrentUserService(
        IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager)
    {
        var principal = httpContextAccessor.HttpContext?.User;

        IsAuthenticated = principal?.Identity?.IsAuthenticated == true;

        if (!IsAuthenticated || principal is null)
            return;

        var userId = userManager.GetUserId(principal);

        if (Guid.TryParse(userId, out var parsedUserId))
            UserId = parsedUserId;

        Email =
            principal.FindFirstValue(ClaimTypes.Email)
            ?? string.Empty;

        DisplayName =
            principal.FindFirstValue("display_name")
            ?? principal.Identity?.Name
            ?? string.Empty;

        Roles = principal
            .FindAll(ClaimTypes.Role)
            .Select(claim => claim.Value)
            .ToArray();
    }
}