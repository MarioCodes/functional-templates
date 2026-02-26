using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BasicAzureFunction;

public class BeatFunction(HealthCheckService healthCheckService)
{
    [Function("Beat")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "beat")] HttpRequest req)
    {
        var result = await healthCheckService.CheckHealthAsync();

        var response = new
        {
            status = result.Status.ToString(),
            timestamp = DateTime.UtcNow,
            checks = result.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                description = e.Value.Description
            })
        };

        return result.Status == HealthStatus.Healthy
            ? new OkObjectResult(response)
            : new ObjectResult(response) { StatusCode = StatusCodes.Status503ServiceUnavailable };
    }
}
