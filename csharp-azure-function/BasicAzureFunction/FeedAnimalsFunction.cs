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
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "animal/{animal}/feed/{feedValue}")] HttpRequest req,
        string animal,
        string feedValue)
    {
        logger.LogInformation("C# HTTP trigger function processed a request.");
        logger.LogInformation($"Feeding {animal} with {feedValue}");

        Animal animalModel = new Animal();
        if(!string.IsNullOrWhiteSpace(animal))
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
            await feeder.Feed(animalModel, feedValueInt);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while feeding the animal.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        return new OkObjectResult($"Animal {animal} has been fed with {feedValueInt} units.");
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