using BudgetPlan.Application.Actions.SettlementPeriods.Commands.CreateSettlementPeriod;
using BudgetPlan.Application.Actions.SettlementPeriods.Commands.UpdateSettlementPeriod;
using BudgetPlan.Application.Actions.SettlementPeriods.Queries.GetSettlementPeriod;
using BudgetPlan.Application.Actions.SettlementPeriods.Queries.GetSettlementPeriods;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetPlan.Api.Controllers;

[Route("api/settlement-periods")]
public sealed class SettlementPeriodsController(ISender sender) : BaseController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetSettlementPeriods(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetSettlementPeriodsQuery(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSettlementPeriod(Guid id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetSettlementPeriodQuery(id), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSettlementPeriod(
        [FromBody] CreateSettlementPeriodCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetSettlementPeriod), new { id = result.Value }, result.Value)
            : HandleFailure(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateSettlementPeriod(
        Guid id,
        [FromBody] UpdateSettlementPeriodCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = id;

        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
}
