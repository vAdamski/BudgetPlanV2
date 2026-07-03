using BudgetPlan.Api.Common.ContractMappers.Auth;
using BudgetPlan.Application.Actions.UserAccountManager.Queries.GetCurentUser;
using BudgetPlan.Contracts.ControllerContracts.Auth.CurrentUser;
using BudgetPlan.Contracts.ControllerContracts.Auth.RegisterUser;
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
    ISender sender)
    : BaseController(sender)
{
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType<CurrentUserResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Me()
    {
        var result = await Sender.Send(new GetCurrentUserQuery(), HttpContext.RequestAborted);

        if (result.IsFailure)
            return HandleFailure(result);

        return Ok(result.Value.ToResponse());
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