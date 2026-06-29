using BudgetPlan.Application.Common.Interfaces.Persistence.Marten;
using BudgetPlan.Application.Common.Interfaces.Persistence.Marten.Repositories;
using BudgetPlan.Persistence.Marten;
using BudgetPlan.Persistence.Marten.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetPlan.Persistence;

public static class DependencyInjection
{
	public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddMartenConfiguration(configuration);
		services.AddIdentityConfiguration(configuration);
		
		services.AddScoped<IAggregateRepository, AggregateRepository>();
		services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();

		return services;
	}
}