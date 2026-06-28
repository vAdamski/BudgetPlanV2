using BudgetPlan.Application.Actions.UserAccountManager.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
