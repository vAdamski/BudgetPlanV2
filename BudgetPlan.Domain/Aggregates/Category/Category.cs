using BudgetPlan.Domain.Common;
using BudgetPlan.Domain.Enums;
using BudgetPlan.Domain.Errors;
using BudgetPlan.Domain.Events;

namespace BudgetPlan.Domain.Aggregates.Category;

public class Category : AggregateBase
{
    public Guid UserId { get; private set; }

    public string Name { get; private set; } = null!;
    public CategoryType Type { get; private set; }

    public bool IsArchived { get; private set; }

    public List<Subcategory> Subcategories { get; private set; } = [];
    
    public Category()
    {
    }

    public static Result<Category> Create(Guid userId, string name, CategoryType type)
    {
        if (string.IsNullOrWhiteSpace(userId.ToString()) || userId == Guid.Empty)
            return Result.Failure<Category>(DomainErrors.Category.InvalidUserId);
            
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Category>(DomainErrors.Category.InvalidName);
            
        var category = new Category();
        
        var categoryCreated = new CategoryEvents.CategoryCreated(Guid.CreateVersion7(), userId, name, type);
        
        category.Apply(categoryCreated);
        category.AddUncommittedEvent(categoryCreated);
        
        return category;
    }
    
    public Result Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            return Result.Failure<Category>(DomainErrors.Category.InvalidName);
        
        if (Name == newName)
            return Result.Success();

        var categoryRenamed = new CategoryEvents.CategoryRenamed(Id, UserId, newName);
        
        Apply(categoryRenamed);
        AddUncommittedEvent(categoryRenamed);
        
        return Result.Success();
    }

    public Result<Guid> AddSubcategory(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Guid>(DomainErrors.Category.InvalidName);
        
        if (Subcategories.Any(x => x.Name == name))
            return Result.Failure<Guid>(DomainErrors.Category.AlreadyExist);
        
        var subcategoryAdded = new CategoryEvents.SubcategoryAdded(Id, UserId, Guid.CreateVersion7(), name);
        
        Apply(subcategoryAdded);
        AddUncommittedEvent(subcategoryAdded);
        
        return Result.Success(subcategoryAdded.SubcategoryId);
    }

    public Result RenameSubcategory(Guid subcategoryId, string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            return Result.Failure<Category>(DomainErrors.Category.InvalidName);
        
        var subcategory = Subcategories.FirstOrDefault(x => x.Id == subcategoryId);
        
        if (subcategory == null)
            return Result.Failure<Category>(DomainErrors.General.NotFound("Subcategory", subcategoryId));
        
        if (subcategory.Name == newName)
            return Result.Success();
        
        var subcategoryRenamed = new CategoryEvents.SubcategoryRenamed(Id, UserId, subcategoryId, newName);
        
        Apply(subcategoryRenamed);
        AddUncommittedEvent(subcategoryRenamed);
        
        return Result.Success();   
    }

    public Result ArchiveSubcategory(Guid subcategoryId)
    {
        var subcategory = Subcategories.FirstOrDefault(x => x.Id == subcategoryId);
        
        if (subcategory == null)
            return Result.Failure<Category>(DomainErrors.General.NotFound("Subcategory", subcategoryId));
        
        if (subcategory.IsArchived)
            return Result.Success();
        
        var subcategoryArchived = new CategoryEvents.SubcategoryArchived(Id, UserId, subcategoryId, DateTime.UtcNow);
        
        Apply(subcategoryArchived);
        AddUncommittedEvent(subcategoryArchived);
        
        return Result.Success();
    }
    
    public Result Archive()
    {
        if (IsArchived)
            return Result.Success();
        
        var categoryArchived = new CategoryEvents.CategoryArchived(Id, UserId, DateTime.UtcNow);
        
        Apply(categoryArchived);
        AddUncommittedEvent(categoryArchived);
        
        return Result.Success();
    }
    
    public void Apply(CategoryEvents.CategoryCreated @event)
    {
        Id = @event.CategoryId;
        UserId = @event.UserId;
        Name = @event.Name;
        Type = @event.Type;
        
        Version++;
    }
    
    public void Apply(CategoryEvents.CategoryRenamed @event)
    {
        Name = @event.Name;
        
        Version++;
    }
    
    public void Apply(CategoryEvents.SubcategoryAdded @event)
    {
        Subcategories.Add(new Subcategory(@event.SubcategoryId, @event.Name, false));
        
        Version++;
    }
    
    public void Apply(CategoryEvents.SubcategoryRenamed @event)
    {
        var subcategoryIndex = Subcategories.FindIndex(x => x.Id == @event.SubcategoryId);
        
        if (subcategoryIndex < 0)
            return;
        
        Subcategories[subcategoryIndex] = Subcategories[subcategoryIndex] with
        {
            Name = @event.Name
        };
        
        Version++;
    }

    public void Apply(CategoryEvents.SubcategoryArchived @event)
    {
        var subcategoryIndex = Subcategories.FindIndex(x => x.Id == @event.SubcategoryId);
        
        if (subcategoryIndex < 0)
            return;

        Subcategories[subcategoryIndex] = Subcategories[subcategoryIndex] with
        {
            IsArchived = true
        };
        
        Version++;
    }

    public void Apply(CategoryEvents.CategoryArchived @event)
    {
        IsArchived = true;
        
        Version++;
    }
}
