using BasicAzureFunction.Services;
using BasicAzureFunction.Services.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using BasicAzureFunction.Options;

var builder = FunctionsApplication.CreateBuilder(args);

// add config for local development and azure environment variables
builder.Configuration
    .AddEnvironmentVariables()
    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

// configure custom options with validation on startup
builder.Services
    .AddOptions<OnlineFeederConf>()
    .Configure<IConfiguration>((opts, conf) =>
    {
        opts.Url = conf["ONLINE_FEEDER_URL"] ?? "";
        opts.TimeoutSeconds = conf.GetValue<int?>("ONLINE_FEEDER_TIMEOUT_SECONDS") ?? 60;
    })
    .Validate(o => !string.IsNullOrWhiteSpace(o.Url), "ONLINE_FEEDER_URL is required")
    .Validate(o => Uri.TryCreate(o.Url, UriKind.Absolute, out _), "ONLINE_FEEDER_URL must be an absolute URL")
    .Validate(o => o.TimeoutSeconds > 0, "ONLINE_FEEDER_TIMEOUT_SECONDS must be > 0")
    .ValidateOnStart();

// register typed HttpClient for IOnlineAnimalFeeder with handler settings
builder.Services
    .AddHttpClient<IOnlineAnimalFeeder, OnlineAnimalFeeder>((services, client) =>
    {
        var options = services.GetRequiredService<IOptions<OnlineFeederConf>>().Value;
        client.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    });

// register health checks
builder.Services.AddHealthChecks();

// register custom services
builder.Services
    .AddSingleton<IAnimalFeeder, AnimalFeeder>();

builder.Build().Run();
