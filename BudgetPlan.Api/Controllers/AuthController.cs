using BudgetPlan.Application.Actions.UserAccountManager.Register;
using BudgetPlan.Application.Common.Interfaces.Api.Services;
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
	[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> Register(
		[FromBody] RegisterUserCommand request,
		CancellationToken cancellationToken)
	{
		var response = await Sender.Send(request, cancellationToken);
		
		return response.IsSuccess ? Ok(response.Value) : HandleFailure(response);
	}
}
