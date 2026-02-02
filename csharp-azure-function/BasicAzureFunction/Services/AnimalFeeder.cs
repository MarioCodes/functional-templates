using BasicAzureFunction.Models;
using BasicAzureFunction.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BasicAzureFunction.Services
{
    // version which reads config directly from IConfiguration (local.settings.json)
    public class AnimalFeeder(ILogger<AnimalFeeder> logger,
        IConfiguration config) : IAnimalFeeder
    {
        public async Task Feed(Animal animal, int value)
        {
            if (animal == null)
            {
                var defaultAnimal = config["DEFAULT_ANIMAL"];
                if (string.IsNullOrWhiteSpace(defaultAnimal))
                {
                    logger.LogWarning("No animal provided and DEFAULT_ANIMAL is not configured.");
                    defaultAnimal = string.Empty;
                }
                animal = new Animal(defaultAnimal);
            }

            // Simulate feeding the animal - out of the scope of this template
            logger.LogInformation($"Feeding animal {animal.Name} with {value} units.");
        }
    }
}