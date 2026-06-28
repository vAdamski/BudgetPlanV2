using BudgetPlan.Api.Services;
using BudgetPlan.Application.Common.Interfaces.Api.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BudgetPlan.Api.Configurations;

public static class DependencyInjection
{
	public static IServiceCollection AddApi(this IServiceCollection services)
	{
		services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		services.TryAddScoped<ICurrentUserService, CurrentUserService>();

		return services;
	}
}
