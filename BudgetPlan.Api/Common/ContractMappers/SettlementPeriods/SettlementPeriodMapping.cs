using BudgetPlan.Application.Actions.SettlementPeriods.Commands.CreateSettlementPeriod;
using BudgetPlan.Application.Actions.SettlementPeriods.Commands.UpdateSettlementPeriod;
using BudgetPlan.Application.Actions.SettlementPeriods.Queries.GetSettlementPeriods;
using BudgetPlan.Contracts.ControllerContracts.SettlementPeriods.CreateSettlementPeriod;
using BudgetPlan.Contracts.ControllerContracts.SettlementPeriods.GetSettlementPeriod;
using BudgetPlan.Contracts.ControllerContracts.SettlementPeriods.GetSettlementPeriods;
using BudgetPlan.Contracts.ControllerContracts.SettlementPeriods.UpdateSettlementPeriod;

namespace BudgetPlan.Api.Common.ContractMappers.SettlementPeriods;

internal static class SettlementPeriodMapping
{
    public static GetSettlementPeriodsResponse ToResponse(this GetSettlementPeriodsResult result)
    {
        return new GetSettlementPeriodsResponse(
            result.SettlementPeriods.Select(x => new SettlementPeriodResponse(
                x.Id,
                x.Name,
                x.Currency,
                x.StartDate,
                x.EndDate)).ToList());
    }

    public static GetSettlementPeriodResponse ToResponse(this SettlementPeriodDto dto)
    {
        return new GetSettlementPeriodResponse(
            dto.Id,
            dto.Name,
            dto.Currency,
            dto.StartDate,
            dto.EndDate);
    }

    public static CreateSettlementPeriodCommand ToCommand(this CreateSettlementPeriodRequest request)
    {
        return new CreateSettlementPeriodCommand(
            request.Name,
            request.Currency,
            request.StartDate,
            request.EndDate);
    }

    public static CreateSettlementPeriodResponse ToResponse(this CreateSettlementPeriodResult result)
    {
        return new CreateSettlementPeriodResponse(result.Id);
    }

    public static UpdateSettlementPeriodCommand ToCommand(this UpdateSettlementPeriodRequest request, Guid id)
    {
        return new UpdateSettlementPeriodCommand(
            id,
            request.Name,
            request.Currency,
            request.StartDate,
            request.EndDate);
    }
}
