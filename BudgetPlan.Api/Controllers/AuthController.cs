using BudgetPlan.Api.Common.ContractMappers.Auth;
using BudgetPlan.Application.Actions.UserAccountManager.Register;
using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Contracts.ControllerContracts.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = currentUserService.UserId;
        var email = currentUserService.Email;
        var displayName = currentUserService.DisplayName;

        return Ok(new { userId, email, displayName });
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
}