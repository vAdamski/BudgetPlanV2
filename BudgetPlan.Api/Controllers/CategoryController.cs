using BudgetPlan.Api.Common.ContractMappers.Category;
using BudgetPlan.Application.Actions.Categories.Commands.DeleteCategory;
using BudgetPlan.Application.Actions.Categories.Commands.DeleteSubcategory;
using BudgetPlan.Application.Actions.Categories.Queries.GetCategories;
using BudgetPlan.Contracts.ControllerContracts.Category.CreateCategory;
using BudgetPlan.Contracts.ControllerContracts.Category.CreateSubcategory;
using BudgetPlan.Contracts.ControllerContracts.Category.RenameCategory;
using BudgetPlan.Contracts.ControllerContracts.Category.RenameSubcategory;
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
        
        if (!result.IsSuccess)
            return HandleFailure(result);
        
        var response = result.Value.ToResponse();
        
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCategory(
        [FromBody] CreateCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var query = request.ToCommand();
        
        var result = await Sender.Send(query, cancellationToken);
        
        if (!result.IsSuccess)
            return HandleFailure(result);
        
        var response = result.Value.ToResponse();
        
        return Created($"/api/categories/{response.Id}", response);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> RenameCategory(
        Guid id,
        [FromBody] RenameCategoryRequest request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request.ToCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new DeleteCategoryCommand(id), cancellationToken);
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
    
    [HttpPost("{categoryId:guid}/subcategories")]
    public async Task<IActionResult> CreateSubcategory(
        Guid categoryId,
        [FromBody] CreateSubcategoryRequest request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request.ToCommand(categoryId), cancellationToken);

        if (!result.IsSuccess)
            return HandleFailure(result);

        var response = result.Value.ToResponse();

        return Created($"/api/categories/{categoryId}/subcategories/{response.Id}", response);
    }
    
    [HttpPut("{categoryId:guid}/subcategories/{subcategoryId:guid}")]
    public async Task<IActionResult> RenameSubcategory(
        Guid categoryId,
        Guid subcategoryId,
        [FromBody] RenameSubcategoryRequest request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request.ToCommand(categoryId, subcategoryId), cancellationToken);
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
    
    [HttpDelete("{categoryId:guid}/subcategories/{subcategoryId:guid}")]
    public async Task<IActionResult> DeleteSubcategory(
        Guid categoryId,
        Guid subcategoryId,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(
            new DeleteSubcategoryCommand(categoryId, subcategoryId),
            cancellationToken);

        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
}
