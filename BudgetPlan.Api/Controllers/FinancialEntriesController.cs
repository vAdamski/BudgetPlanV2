using BudgetPlan.Application.Actions.FinancialEntries.Commands.CreateFinancialEntry;
using BudgetPlan.Application.Actions.FinancialEntries.Commands.DeleteFinancialEntry;
using BudgetPlan.Application.Actions.FinancialEntries.Commands.UpdateFinancialEntry;
using BudgetPlan.Application.Actions.FinancialEntries.Queries.GetFinancialEntries;
using BudgetPlan.Application.Actions.FinancialEntries.Queries.GetFinancialEntry;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetPlan.Api.Controllers;

[Route("api/financial-entries")]
public sealed class FinancialEntriesController(ISender sender) : BaseController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetFinancialEntries(
        [FromQuery] GetFinancialEntriesQuery query,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetFinancialEntry(Guid id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetFinancialEntryQuery(id), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFinancialEntry(
        [FromBody] CreateFinancialEntryCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetFinancialEntry), new { id = result.Value }, result.Value)
            : HandleFailure(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateFinancialEntry(
        Guid id,
        [FromBody] UpdateFinancialEntryCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = id;

        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteFinancialEntry(Guid id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new DeleteFinancialEntryCommand { Id = id }, cancellationToken);
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
}
