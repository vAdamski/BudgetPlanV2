using FluentValidation;

namespace BudgetPlan.Application.Actions.Categories.Commands.DeleteSubcategory;

public class DeleteSubcategoryCommandValidator : AbstractValidator<DeleteSubcategoryCommand>
{
    public DeleteSubcategoryCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category Id is required.");

        RuleFor(x => x.SubcategoryId)
            .NotEmpty().WithMessage("Subcategory Id is required.");
    }
}
