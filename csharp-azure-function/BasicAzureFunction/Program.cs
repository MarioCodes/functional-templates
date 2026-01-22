using BasicAzureFunction.Services;
using BasicAzureFunction.Services.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

// add config for local development and azure environment variables
builder.Configuration
    .AddEnvironmentVariables()
    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

// custom services
builder.Services
    .AddSingleton<IAnimalFeeder, AnimalFeeder>();

builder.Build().Run();
