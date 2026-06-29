using BudgetPlan.Application.Actions.Categories.Commands.CreateCategory;
using BudgetPlan.Application.Actions.Categories.Commands.CreateSubcategory;
using BudgetPlan.Application.Actions.Categories.Commands.DeleteCategory;
using BudgetPlan.Application.Actions.Categories.Commands.DeleteSubcategory;
using BudgetPlan.Application.Actions.Categories.Commands.RenameCategory;
using BudgetPlan.Application.Actions.Categories.Commands.RenameSubcategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetPlan.Api.Controllers;

[Route("api/categories")]
public class CategoryController(ISender sender) : BaseController(sender)
{
    // [HttpGet]
    // public async Task<IAsyncResult> GetCategories()
    // {
    //     var result = await Sender.Send(new GetCategoriesQuery());
    //     return Ok(result);
    // }
    
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        var result = await Sender.Send(command);
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> RenameCategory([FromBody] RenameCategoryCommand command)
    {
        var result = await Sender.Send(command);
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryCommand command)
    {
        var result = await Sender.Send(command);
        return result.IsSuccess ? Ok() : HandleFailure(result);
    }
    
    [HttpPost("subcategories")]
    public async Task<IActionResult> CreateSubcategory([FromBody] CreateSubcategoryCommand command)
    {
        var result = await Sender.Send(command);
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }
    
    [HttpPut("subcategories")]
    public async Task<IActionResult> RenameSubcategory([FromBody] RenameSubcategoryCommand command)
    {
        var result = await Sender.Send(command);
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }
    
    [HttpDelete("subcategories")]
    public async Task<IActionResult> DeleteSubcategory([FromBody] DeleteSubcategoryCommand command)
    {
        var result = await Sender.Send(command);
        return result.IsSuccess ? Ok() : HandleFailure(result);
    }
}