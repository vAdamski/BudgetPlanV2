using BudgetPlan.Application.Actions.HealthActions.Queries.CheckHealth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetPlan.Api.Controllers;

[Route("api/health")]
public sealed class HealthController(ISender sender) : BaseController(sender)
{
	[HttpGet("anonymous")]
	[AllowAnonymous]
	public async Task<IActionResult> Get(CancellationToken cancellationToken)
	{
		var result = await Sender.Send(new CheckHealthQuery(), cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
	
	[HttpGet("authorized")]
	public async Task<IActionResult> GetAuthorized(CancellationToken cancellationToken)
	{
		var result = await Sender.Send(new CheckHealthQuery(), cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
}
