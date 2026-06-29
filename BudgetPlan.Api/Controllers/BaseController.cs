using BudgetPlan.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetPlan.Api.Controllers;

[ApiController]
[Authorize]
public class BaseController : ControllerBase
{
	protected readonly ISender Sender;

	protected BaseController(ISender sender) => Sender = sender;

	protected IActionResult HandleFailure(Result result) =>
		result switch
		{
			{ IsSuccess: true } => throw new InvalidOperationException(),
			IValidationResult validationResult =>
				BadRequest(
					CreateProblemDetails(
						"Validation Error",
						StatusCodes.Status400BadRequest,
						result.Error,
						validationResult.Errors)),
			_ =>
				StatusCode(
					GetStatusCode(result.Error),
					CreateProblemDetails(
						GetTitle(result.Error),
						GetStatusCode(result.Error),
						result.Error))
		};

	private static int GetStatusCode(Error error)
	{
		if (error.Code.EndsWith("NotFound", StringComparison.Ordinal) ||
		    error.Code == "General.NotFound")
			return StatusCodes.Status404NotFound;

		if (error.Code.EndsWith("AccessDenied", StringComparison.Ordinal))
			return StatusCodes.Status403Forbidden;

		return StatusCodes.Status400BadRequest;
	}

	private static string GetTitle(Error error) =>
		GetStatusCode(error) switch
		{
			StatusCodes.Status404NotFound => "Not Found",
			StatusCodes.Status403Forbidden => "Forbidden",
			_ => "Bad Request"
		};

	private static ProblemDetails CreateProblemDetails(
		string title,
		int status,
		Error error,
		Error[]? errors = null) =>
		new()
		{
			Title = title,
			Type = error.Code,
			Detail = error.Message,
			Status = status,
			Extensions = { { nameof(errors), errors } }
		};
}
