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
builder.Services.AddIdentityApiEndpoints<ApplicationUser>(options =>
    {
        options.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<IdentityDbContext>();

var app = builder.Build();


app.BockEndpoints(new List<EndpointsBlocker.BlockedEndpoint>
{
    new("/register", HttpMethod.Post)
});

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.MapIdentityApi<ApplicationUser>();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();