using BudgetPlan.Application.Common.Interfaces.Persistence;
using BudgetPlan.Application.Common.Interfaces.Persistence.Marten;
using BudgetPlan.Domain.Entities;
using BudgetPlan.Persistence.Factories;
using BudgetPlan.Persistence.Health;
using BudgetPlan.Persistence.Marten;
using JasperFx;
using JasperFx.Events;
using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetPlan.Persistence;

public static class DependencyInjection
{
	public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
	{
		AddMarten(services, configuration);
		AddIdentity(services, configuration);
		
		services.AddScoped<IAggregateRepository, AggregateRepository>();

		return services;
	}

	private static void AddMarten(this IServiceCollection services, IConfiguration configuration)
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
		}).UseLightweightSessions();
	}

	private static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
	{
		var identityDbConnectionString = Environment.GetEnvironmentVariable("IDENTITY_DB_CONNECTION_STRING")
		                                 ?? configuration.GetConnectionString("IdentityDbConnection");
		
		if (string.IsNullOrWhiteSpace(identityDbConnectionString))
		{
			throw new InvalidOperationException("Database connection string is not configured.");
		}
		
		services.AddDbContext<IdentityDbContext>(options =>
		{
			options.UseNpgsql(identityDbConnectionString,
				postgres => postgres.MigrationsHistoryTable(
					"__EFMigrationsHistory",
					"identity"));
		});

		services.AddScoped<IDatabaseHealthCheck, MartenDatabaseHealthCheck>();

		services.AddDataProtection();
		
		services
			.AddIdentityApiEndpoints<ApplicationUser>(options =>
			{
				options.User.RequireUniqueEmail = true;

				options.SignIn.RequireConfirmedEmail = true;

				options.Password.RequiredLength = 8;
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireNonAlphanumeric = true;

				options.Lockout.AllowedForNewUsers = true;
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.DefaultLockoutTimeSpan =
					TimeSpan.FromMinutes(15);
			})
			.AddRoles<IdentityRole<Guid>>()
			.AddEntityFrameworkStores<IdentityDbContext>()
			.AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>();
	}
}