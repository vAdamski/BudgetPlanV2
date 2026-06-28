using BudgetPlan.Api.Configurations;
using BudgetPlan.Api.Extensions;
using BudgetPlan.Application;
using BudgetPlan.Domain.Entities;
using BudgetPlan.Infrastructure;
using BudgetPlan.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilog();

builder.Services.AddApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.BockEndpoints(new List<EndpointsBlocker.BlockedEndpoint>
{
    new("/register", HttpMethod.Post)
});

app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<ApplicationUser>();
app.MapControllers();

app.Run();