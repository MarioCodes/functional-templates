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
                string defaultAnimal = config["DEFAULT_ANIMAL"] ?? "";
                // Log a warning if the default animal is not set
                animal = new Animal(defaultAnimal);
            }

            // Simulate feeding the animal - out of the scope of this template

        }
    }
}