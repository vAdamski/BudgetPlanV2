using BudgetPlan.Domain.Common;
using FluentValidation;
using MediatR;

namespace BudgetPlan.Application.Common.Behaviours;

public sealed class ValidationPipelineBehavior<TRequest, TResponse>
	: IPipelineBehavior<TRequest, TResponse>
	where TRequest : notnull, IRequest<TResponse>
	where TResponse : Result
{
	private readonly IEnumerable<IValidator<TRequest>> _validators;

	public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) =>
		_validators = validators;

	public async Task<TResponse> Handle(
		TRequest request,
		RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		if (!_validators.Any())
		{
			return await next();
		}

		var validationResults = await Task.WhenAll(
			_validators.Select(validator => validator.ValidateAsync(request, cancellationToken)));

		Error[] errors = validationResults
			.SelectMany(validationResult => validationResult.Errors)
			.Where(validationFailure => validationFailure is not null)
			.Select(failure => new Error(failure.PropertyName, failure.ErrorMessage))
			.Distinct()
			.ToArray();

		return errors.Length == 0
			? await next()
			: CreateValidationResult<TResponse>(errors);
	}

	private static TResult CreateValidationResult<TResult>(Error[] errors)
		where TResult : Result
	{
		if (typeof(TResult) == typeof(Result))
		{
			return (ValidationResult.WithErrors(errors) as TResult)!;
		}

		object validationResult = typeof(ValidationResult<>)
			.GetGenericTypeDefinition()
			.MakeGenericType(typeof(TResult).GenericTypeArguments[0])
			.GetMethod(nameof(ValidationResult.WithErrors))!
			.Invoke(null, [errors])!;

		return (TResult)validationResult;
	}
}
