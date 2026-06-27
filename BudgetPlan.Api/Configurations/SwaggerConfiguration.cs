using Microsoft.OpenApi;

namespace BudgetPlan.Api.Configurations;

public static class SwaggerConfiguration
{
	public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
	{
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "BudgetPlan API",
				Version = "v1"
			});
		});

		return services;
	}

	public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
	{
		app.UseSwagger();
		app.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint("/swagger/v1/swagger.json", "BudgetPlan API V1");
			c.RoutePrefix = string.Empty;
		});

		return app;
	}
}
