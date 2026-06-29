using BudgetPlan.Application.Common.Interfaces.Api.Services;
using BudgetPlan.Application.Common.Interfaces.Persistence.Marten;
using BudgetPlan.Domain.Aggregates.Category;
using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Common.Abstractions.Messaging;

namespace BudgetPlan.Application.Actions.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler(
    ICurrentUserService currentUserService,
    IAggregateRepository aggregateRepository) : ICommandHandler<CreateCategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = Category.Create(currentUserService.UserId, command.Name, command.Type);
        
        if (category.IsFailure)
            return Result.Failure<Guid>(category.Error);
        
        await aggregateRepository.StoreAsync(category.Value, cancellationToken);
        
        return Result.Success(category.Value.Id);
    }
}