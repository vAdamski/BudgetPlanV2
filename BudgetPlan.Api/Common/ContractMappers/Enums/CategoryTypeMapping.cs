namespace BudgetPlan.Api.Common.ContractMappers.Enums;

internal static class CategoryTypeMapping
{
    public static Domain.Enums.CategoryType ToDomain(this Contracts.Enums.CategoryType categoryType)
    {
        return categoryType switch
        {
            Contracts.Enums.CategoryType.Income => Domain.Enums.CategoryType.Income,
            Contracts.Enums.CategoryType.Expense => Domain.Enums.CategoryType.Expense,
            _ => throw new ArgumentOutOfRangeException(nameof(categoryType), categoryType, null)
        };
    }
}