using BudgetPlan.Api.Common.ContractMappers.FinancialEntries;
using BudgetPlan.Application.Actions.FinancialEntries.Commands.CreateFinancialEntry;
using BudgetPlan.Application.Actions.FinancialEntries.Commands.DeleteFinancialEntry;
using BudgetPlan.Application.Actions.FinancialEntries.Commands.UpdateFinancialEntry;
using BudgetPlan.Application.Actions.FinancialEntries.Queries.GetFinancialEntries;
using BudgetPlan.Application.Actions.FinancialEntries.Queries.GetFinancialEntry;
using BudgetPlan.Contracts.ControllerContracts.FinancialEntries.CreateFinancialEntry;
using BudgetPlan.Contracts.ControllerContracts.FinancialEntries.GetFinancialEntries;
using BudgetPlan.Contracts.ControllerContracts.FinancialEntries.UpdateFinancialEntry;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetPlan.Api.Controllers;

[Route("api/financial-entries")]
public sealed class FinancialEntriesController(ISender sender) : BaseController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetFinancialEntries(
        [FromQuery] GetFinancialEntriesRequest request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request.ToQuery(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value.ToResponse()) : HandleFailure(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetFinancialEntry(Guid id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetFinancialEntryQuery(id), cancellationToken);
        return result.IsSuccess ? Ok(result.Value.ToResponse()) : HandleFailure(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFinancialEntry(
        [FromBody] CreateFinancialEntryRequest request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request.ToCommand(), cancellationToken);

        if (!result.IsSuccess)
            return HandleFailure(result);

        var response = result.Value.ToResponse();

        return CreatedAtAction(nameof(GetFinancialEntry), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateFinancialEntry(
        Guid id,
        [FromBody] UpdateFinancialEntryRequest request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request.ToCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteFinancialEntry(Guid id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new DeleteFinancialEntryCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
}
