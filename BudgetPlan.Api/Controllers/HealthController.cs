using BudgetPlan.Application.Actions.HealthActions.Queries.CheckHealth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetPlan.Api.Controllers;

[Route("api/health")]
public sealed class HealthController(ISender sender) : BaseController(sender)
{
	[HttpGet]
	public async Task<IActionResult> Get(CancellationToken cancellationToken)
	{
		var result = await Sender.Send(new CheckHealthQuery(), cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
	}
}
