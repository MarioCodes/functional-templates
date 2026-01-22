using BasicAzureFunction.Services;
using BasicAzureFunction.Services.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = FunctionsApplication.CreateBuilder(args);

// add config for local development and azure environment variables
builder.Configuration
    .AddEnvironmentVariables()
    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

// register HttpClient factory and configure online feeder client
builder.Services.AddHttpClient("OnlineFeeder", (service, client) =>
{
    var config = service.GetRequiredService<IConfiguration>();
    var logger = service.GetRequiredService<ILogger<Program>>();

    string? url = config["ONLINE_FEEDER_URL"];
    if (string.IsNullOrWhiteSpace(url))
    {
        var msg = "Required configuration 'ONLINE_FEEDER_URL' is missing or empty";
        logger.LogError(msg);
        throw new InvalidOperationException(msg);
    }

    int timeoutSeconds = config.GetValue<int?>("ONLINE_FEEDER_TIMEOUT_SECONDS") ?? 60;
    client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
    client.BaseAddress = new Uri(url);
});

// register service which uses the previously configured HttpClient
builder.Services.AddSingleton<IOnlineAnimalFeeder, OnlineAnimalFeeder>(service =>
{
    var config = service.GetRequiredService<IConfiguration>();
    var logger = service.GetRequiredService<ILogger<OnlineAnimalFeeder>>();
    
    var httpClientFactory = service.GetRequiredService<IHttpClientFactory>();
    var client = httpClientFactory.CreateClient("OnlineFeeder");
    return new OnlineAnimalFeeder(logger, config, client);
});

// register custom services
builder.Services
    .AddSingleton<IAnimalFeeder, AnimalFeeder>();

builder.Build().Run();
