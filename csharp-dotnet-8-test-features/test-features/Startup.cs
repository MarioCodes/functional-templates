using testFeatures.Configuration;
using testFeatures.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.interfaces;
using testFeatures.Middleware;
using testFeatures.Middleware.interfaces;

namespace testFeatures
{
    public class Startup
    {

        public IConfiguration _config { get; }

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Service Configuration
            services.AddControllers();
            services.AddSwaggerGen();

            // Services (scoped as they use appcontext - EF Core)
            services.AddScoped<IUserService, UserService>();

            // middleware - first part -> dependant on app.UseMiddleware
            services.AddTransient<IMiddlewareService, MiddlewareService>();
            services.AddTransient<CustomMiddleware>();

            // add health checks - first part -> dependant on app.UseHealthChecks
            services.AddHealthChecks();

            // Database
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: "InMemoryTestDatabase"));

            // Custom Configurations
            services.Configure<UserConfig>(_config.GetSection(UserConfig.Section));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            // register middleware <- has to be before ¡app.UseEndpoints()!
            app.UseMiddleware<CustomMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // register concrete endpoint for health checks
            app.UseHealthChecks("/beat");
        }
    }
}
