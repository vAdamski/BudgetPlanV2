using FluentValidation;

namespace BudgetPlan.Application.Actions.Categories.Commands.DeleteSubcategory;

public class DeleteSubcategoryCommandValidator : AbstractValidator<DeleteSubcategoryCommand>
{
    public DeleteSubcategoryCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}