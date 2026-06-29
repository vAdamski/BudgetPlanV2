using FluentValidation;

namespace BudgetPlan.Application.Actions.Categories.Commands.CreateSubcategory;

public class CreateSubcategoryCommandValidator : AbstractValidator<CreateSubcategoryCommand>
{
    public CreateSubcategoryCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Subcategory name is required.")
            .MaximumLength(100).WithMessage("Subcategory name must not exceed 100 characters.");
    }
}