using BasicAzureFunction.Models;
using BasicAzureFunction.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BasicAzureFunction.Services
{
    // version which gets an HttpClient injected (configured in Program.cs)
    public class OnlineAnimalFeeder(ILogger<OnlineAnimalFeeder> logger,
        IConfiguration config,
        HttpClient client) : IOnlineAnimalFeeder
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
