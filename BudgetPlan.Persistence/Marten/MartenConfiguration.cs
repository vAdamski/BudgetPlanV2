using JasperFx;
using JasperFx.Events;
using JasperFx.Events.Daemon;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetPlan.Persistence.Marten;

public static class MartenConfiguration
{
    public static IServiceCollection AddMartenConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var applicationDbConnectionString = Environment.GetEnvironmentVariable("APPLICATION_DB_CONNECTION_STRING")
                                            ?? configuration.GetConnectionString("ApplicationDbConnection");
		
        if (string.IsNullOrWhiteSpace(applicationDbConnectionString))
        {
            throw new InvalidOperationException("Database connection string is not configured.");
        }
		
        var schemaName = configuration["Marten:SchemaName"] ?? "budget_plan";
        var autoCreateSchemaObjects = !bool.TryParse(configuration["Marten:AutoCreateSchemaObjects"], out var enabled)
                                      || enabled;
		
        services.AddMarten(options =>
            {
                options.Events.StreamIdentity = StreamIdentity.AsGuid;
                options.Connection(applicationDbConnectionString);
                options.DatabaseSchemaName = schemaName;
                options.AutoCreateSchemaObjects = autoCreateSchemaObjects
                    ? AutoCreate.CreateOrUpdate
                    : AutoCreate.None;

                options.ConfigureProjections();
            }).UseLightweightSessions()
            .AddAsyncDaemon(DaemonMode.HotCold);
        
        return services;
    }
}