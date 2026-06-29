using FluentValidation;

namespace BudgetPlan.Application.Actions.Categories.Commands.RenameSubcategory;

public class RenameSubcategoryCommandValidator : AbstractValidator<RenameSubcategoryCommand>
{
    public RenameSubcategoryCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category Id is required.");

        RuleFor(x => x.SubcategoryId)
            .NotEmpty().WithMessage("Subcategory Id is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Subcategory name is required.")
            .MaximumLength(100).WithMessage("Subcategory name must not exceed 100 characters.");
    }
}
