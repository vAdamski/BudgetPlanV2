using BudgetPlan.Application.Common.Interfaces.Persistence;
using BudgetPlan.Domain.Entities;
using BudgetPlan.Persistence.Factories;
using BudgetPlan.Persistence.Health;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetPlan.Persistence;

public static class IdentityConfiguration
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
        IConfiguration configuration)
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
        
        return services;
    }
    
    public static IEndpointRouteBuilder MapApplicationIdentityApi(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints
            .MapGroup("/identity")
            .MapIdentityApi<ApplicationUser>();

        return endpoints;
    }
}