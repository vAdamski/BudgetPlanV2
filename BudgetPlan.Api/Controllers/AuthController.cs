using BudgetPlan.Api.Common.ContractMappers.Auth;
using BudgetPlan.Application.Actions.UserAccountManager.Register;
using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Contracts.ControllerContracts.Auth;
using BudgetPlan.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BudgetPlan.Api.Controllers;

[ApiController]
[Route("api/auth")]
[Tags("Auth")]
[Produces("application/json")]
public sealed class AuthController(
    ISender sender,
    ICurrentUserService currentUserService)
    : BaseController(sender)
{
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType<CurrentUserResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Me([FromServices] UserManager<ApplicationUser> userManager)
    {
        var applicationUser =
            await userManager.GetUserAsync(User);

        if (applicationUser is null)
            return Unauthorized();

        var roles =
            await userManager.GetRolesAsync(applicationUser);

        return Ok(new CurrentUserResponse(
            applicationUser.Id,
            applicationUser.Email!,
            applicationUser.DisplayName,
            roles.ToArray()));
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(
            request.ToCommand(),
            cancellationToken);

        if (result.IsFailure)
            return HandleFailure(result);

        return Ok(result.Value.ToResponse());
    }
    
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout(
        [FromServices]
        SignInManager<ApplicationUser> signInManager)
    {
        await signInManager.SignOutAsync();

        return NoContent();
    }
}