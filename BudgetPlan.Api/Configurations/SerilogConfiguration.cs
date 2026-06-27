using Serilog;

namespace BudgetPlan.Api.Configurations;

public static class SerilogConfiguration
{
	public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
	{
		builder.Host.UseSerilog((context, services, configuration) =>
		{
			configuration
				.ReadFrom.Configuration(context.Configuration)
				.ReadFrom.Services(services)
				.Enrich.FromLogContext();
		});

		return builder;
	}
}
