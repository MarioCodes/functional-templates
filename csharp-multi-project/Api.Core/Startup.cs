using Api.Core.Configuration;
using Api.Core.Services;
using Api.Core.Services.interfaces;
using Api.External.Consumer.Common;
using Api.External.Consumer.Common.Interfaces;
using Api.External.Consumer.Services;
using Api.External.Consumer.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace apicore
{
    public class Startup
    {

        public IConfiguration _config { get; }

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Service Configuration
            services.AddControllers();
            services.AddSwaggerGen();

            // Services
            services.AddScoped<ISlotsService, SlotsService>();
            services.AddScoped<IExternalApiService, ExternalApiService>();
            services.AddScoped<IHttpService, HttpService>();
            services.AddHttpClient<ExternalApiService>(c => c.BaseAddress = new System.Uri("https://draliatest.azurewebsites.net/api"));

            // add health checks
            services.AddHealthChecks();

            // Custom Configurations
            services.Configure<ExternalApiConfig>(_config.GetSection(ExternalApiConfig.Section));
            services.Configure<AuthConfig>(_config.GetSection(AuthConfig.Section));
            services.Configure<CoreConfig>(_config.GetSection(CoreConfig.Section));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // register concrete endpoint for health checks
            app.UseHealthChecks("/beat");
        }
    }
}
