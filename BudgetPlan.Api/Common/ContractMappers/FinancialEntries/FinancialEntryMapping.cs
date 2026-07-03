using BudgetPlan.Api.Common.ContractMappers.Enums;
using BudgetPlan.Application.Actions.FinancialEntries.Commands.CreateFinancialEntry;
using BudgetPlan.Application.Actions.FinancialEntries.Commands.UpdateFinancialEntry;
using BudgetPlan.Application.Actions.FinancialEntries.Queries.GetFinancialEntries;
using BudgetPlan.Contracts.ControllerContracts.FinancialEntries.CreateFinancialEntry;
using BudgetPlan.Contracts.ControllerContracts.FinancialEntries.GetFinancialEntries;
using BudgetPlan.Contracts.ControllerContracts.FinancialEntries.GetFinancialEntry;
using BudgetPlan.Contracts.ControllerContracts.FinancialEntries.UpdateFinancialEntry;

namespace BudgetPlan.Api.Common.ContractMappers.FinancialEntries;

internal static class FinancialEntryMapping
{
    public static GetFinancialEntriesQuery ToQuery(this GetFinancialEntriesRequest request)
    {
        return new GetFinancialEntriesQuery(
            request.OccurredFrom,
            request.OccurredTo,
            request.CategoryId,
            request.SubcategoryId,
            request.Type?.ToDomain());
    }

    public static GetFinancialEntriesResponse ToResponse(this GetFinancialEntriesResult result)
    {
        return new GetFinancialEntriesResponse(
            result.FinancialEntries.Select(x => new FinancialEntryResponse(
                x.Id,
                x.CategoryId,
                x.SubcategoryId,
                x.Type.ToContract(),
                x.Amount,
                x.OccurredOn,
                x.IsDeleted)).ToList());
    }

    public static GetFinancialEntryResponse ToResponse(this FinancialEntryDto dto)
    {
        return new GetFinancialEntryResponse(
            dto.Id,
            dto.CategoryId,
            dto.SubcategoryId,
            dto.Type.ToContract(),
            dto.Amount,
            dto.OccurredOn,
            dto.IsDeleted);
    }

    public static CreateFinancialEntryCommand ToCommand(this CreateFinancialEntryRequest request)
    {
        return new CreateFinancialEntryCommand(
            request.CategoryId,
            request.SubcategoryId,
            request.Amount,
            request.OccurredOn);
    }

    public static CreateFinancialEntryResponse ToResponse(this CreateFinancialEntryResult result)
    {
        return new CreateFinancialEntryResponse(result.Id);
    }

    public static UpdateFinancialEntryCommand ToCommand(this UpdateFinancialEntryRequest request, Guid id)
    {
        return new UpdateFinancialEntryCommand(
            id,
            request.CategoryId,
            request.SubcategoryId,
            request.Amount,
            request.OccurredOn);
    }
}
