using BasicAzureFunction.Models;
using BasicAzureFunction.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BasicAzureFunction.Options;

namespace BasicAzureFunction.Services
{
    // version which gets an HttpClient injected (configured in Program.cs)
    public class OnlineAnimalFeeder(ILogger<OnlineAnimalFeeder> logger,
        IConfiguration config,
        IOptions<OnlineFeederConf> feederOpt,
        HttpClient client) : IOnlineAnimalFeeder
    {
        private readonly OnlineFeederConf feederConf = feederOpt.Value;

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

            // Simulate feeding the animal - call the external API endpoint
            logger.LogInformation("Online feeder sending feed for {Animal} with {Value} units.", animal.Name, value);

            string? urlTemplate = feederConf.Url;
            if (string.IsNullOrWhiteSpace(urlTemplate))
            {
                logger.LogError("OnlineFeederOptions.Url is not configured.");
                return;
            }

            var resolvedUrl = urlTemplate
                .Replace("{animal}", Uri.EscapeDataString(animal.Name ?? string.Empty))
                .Replace("{value}", Uri.EscapeDataString(value.ToString()));

            if (!Uri.TryCreate(resolvedUrl, UriKind.Absolute, out var requestUri))
            {
                logger.LogError("Invalid request URL after substitution: {Url}", resolvedUrl);
                return;
            }

            try
            {
                using var response = await client.GetAsync(requestUri).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    logger.LogInformation("Feed request succeeded for {Animal} with {Value}. StatusCode: {StatusCode}", animal.Name, value, (int)response.StatusCode);
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    logger.LogWarning("Feed request failed for {Animal} with {Value}. StatusCode: {StatusCode}. Content: {Content}", animal.Name, value, (int)response.StatusCode, content);
                }
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, "HTTP request error while feeding {Animal} with {Value}.", animal.Name, value);
            }
            catch (TaskCanceledException ex)
            {
                logger.LogError(ex, "HTTP request timed out while feeding {Animal} with {Value}.", animal.Name, value);
            }
        }
    }
}
