namespace BudgetPlan.Api.Extensions;

public static class EndpointsBlocker
{
    /// <summary>
    /// Blocks specified endpoints by returning a 404 Not Found response for matching requests.
    /// </summary>
    /// <param name="app">The <see cref="blockedEndpoints"/> instance.</param>
    /// <param name="blockedEndpoints">A list of <see cref="IApplicationBuilder"/> objects representing the endpoints to block.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder BockEndpoints(this IApplicationBuilder app, List<BlockedEndpoint> blockedEndpoints)
    {
        app.Use(async (context, next) =>
        {
            var shouldBeBlocked = blockedEndpoints.Any(x =>
                string.Equals(context.Request.Path.Value, x.Path, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(context.Request.Method, x.Method.Method, StringComparison.OrdinalIgnoreCase));

            if (shouldBeBlocked)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }

            await next();
        });
        
        return app;
    }

    /// <summary>
    /// Represents a blocked endpoint.
    /// </summary>
    /// <param name="Path">The path of the blocked endpoint.</param>
    /// <param name="Method">The HTTP method of the blocked endpoint.</param>
    public record BlockedEndpoint(string Path, HttpMethod Method);
}