using BudgetPlan.Application.Actions.Categories.Commands.CreateCategory;
using BudgetPlan.Application.Actions.Categories.Commands.CreateSubcategory;
using BudgetPlan.Application.Actions.Categories.Commands.DeleteCategory;
using BudgetPlan.Application.Actions.Categories.Commands.DeleteSubcategory;
using BudgetPlan.Application.Actions.Categories.Commands.RenameCategory;
using BudgetPlan.Application.Actions.Categories.Commands.RenameSubcategory;
using BudgetPlan.Application.Actions.Categories.Queries.GetCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetPlan.Api.Controllers;

[Route("api/categories")]
public sealed class CategoryController(ISender sender) : BaseController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetCategoriesQuery(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCategory(
        [FromBody] CreateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Created($"/api/categories/{result.Value}", result.Value)
            : HandleFailure(result);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> RenameCategory(
        Guid id,
        [FromBody] RenameCategoryCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = id;

        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new DeleteCategoryCommand { Id = id }, cancellationToken);
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
    
    [HttpPost("{categoryId:guid}/subcategories")]
    public async Task<IActionResult> CreateSubcategory(
        Guid categoryId,
        [FromBody] CreateSubcategoryCommand command,
        CancellationToken cancellationToken)
    {
        command.CategoryId = categoryId;

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Created($"/api/categories/{categoryId}/subcategories/{result.Value}", result.Value)
            : HandleFailure(result);
    }
    
    [HttpPut("{categoryId:guid}/subcategories/{subcategoryId:guid}")]
    public async Task<IActionResult> RenameSubcategory(
        Guid categoryId,
        Guid subcategoryId,
        [FromBody] RenameSubcategoryCommand command,
        CancellationToken cancellationToken)
    {
        command.CategoryId = categoryId;
        command.SubcategoryId = subcategoryId;

        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
    
    [HttpDelete("{categoryId:guid}/subcategories/{subcategoryId:guid}")]
    public async Task<IActionResult> DeleteSubcategory(
        Guid categoryId,
        Guid subcategoryId,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(
            new DeleteSubcategoryCommand
            {
                CategoryId = categoryId,
                SubcategoryId = subcategoryId
            },
            cancellationToken);

        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
}
