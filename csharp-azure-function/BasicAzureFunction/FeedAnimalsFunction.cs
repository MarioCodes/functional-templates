using BasicAzureFunction.Models;
using BasicAzureFunction.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BasicAzureFunction;

public class FeedAnimalsFunction(ILogger<FeedAnimalsFunction> logger,
    IAnimalFeeder feeder)
{

    [Function("FeedAnimal")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "animal/{animal}/feed/{feedValue}")] HttpRequest req)
    {
        logger.LogInformation("C# HTTP trigger function processed a request.");

        // read values from route
        string animalStr = req.RouteValues["animal"]?.ToString() ?? "";
        string feedValue = req.RouteValues["feedValue"]?.ToString() ?? "";
        logger.LogInformation($"Feeding {animalStr} with {feedValue}");

        Animal animal = new Animal();
        if(!string.IsNullOrWhiteSpace(animalStr))
        {
            // map somehow animal to its model
        }

        int feedValueInt;
        if (!TryInt(feedValue, out feedValueInt))
        {
            logger.LogWarning("Invalid feedValue provided. Defaulting to 0.");
            feedValueInt = 0;
        }

        try
        {
            feeder.Feed(animal, feedValueInt);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while feeding the animal.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        return new OkObjectResult($"Animal {animalStr} has been fed with {feedValueInt} units.");
    }

    private static bool TryInt(string? value, out int result)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = 0;
            return false;
        }

        return int.TryParse(value, out result);
    }
}