using BudgetPlan.Api.Common.ContractMappers.SettlementPeriods;
using BudgetPlan.Application.Actions.SettlementPeriods.Queries.GetSettlementPeriod;
using BudgetPlan.Application.Actions.SettlementPeriods.Queries.GetSettlementPeriods;
using BudgetPlan.Contracts.ControllerContracts.SettlementPeriods.CreateSettlementPeriod;
using BudgetPlan.Contracts.ControllerContracts.SettlementPeriods.UpdateSettlementPeriod;
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
        return result.IsSuccess ? Ok(result.Value.ToResponse()) : HandleFailure(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSettlementPeriod(Guid id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetSettlementPeriodQuery(id), cancellationToken);
        return result.IsSuccess ? Ok(result.Value.ToResponse()) : HandleFailure(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSettlementPeriod(
        [FromBody] CreateSettlementPeriodRequest request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request.ToCommand(), cancellationToken);

        if (!result.IsSuccess)
            return HandleFailure(result);

        var response = result.Value.ToResponse();

        return CreatedAtAction(nameof(GetSettlementPeriod), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateSettlementPeriod(
        Guid id,
        [FromBody] UpdateSettlementPeriodRequest request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request.ToCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
}
