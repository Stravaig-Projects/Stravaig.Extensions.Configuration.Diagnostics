using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using Stravaig.Configuration.Diagnostics;
using Stravaig.Configuration.Diagnostics.Logging;
using Stravaig.Configuration.Diagnostics.Serilog;

namespace Example
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = (IConfigurationRoot)configuration;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigurationDiagnosticsOptions.SetupBy
                .Obfuscating.WithAsterisks()
                .And.MatchingConfigurationKeys.Where
                .KeyContains("AccessToken")
                .OrContains("ConnectionString")
                .And.MatchingConnectionStringKeys
                .Containing("Password")
                .AndFinally.ApplyOptions(ConfigurationDiagnosticsOptions.GlobalOptions);

            // Using the serilog logger you can log earlier
            // (without footering around with the Microsoft logging framework)
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger().ForContext<Startup>();
            logger.Information("This is the Serilog version.");
            logger.LogProvidersAsInformation(Configuration);
            logger.LogConfigurationValuesAsInformation(Configuration);
            logger.LogConfigurationKeySourceAsInformation(Configuration, "ExtenalSystem:AccessToken"); // This is deliberately misspelled.
            logger.LogAllConnectionStringsAsInformation(Configuration);

            
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Example", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // If you don't want to install an additional logging framework
            // and want to use the Microsoft logging extensions, then you can
            // log from this point easily as you have access to a service provider.
            var logFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
            var logger = logFactory.CreateLogger<Startup>();
            logger.LogInformation("This is the Microsoft logging extensions version.");
            logger.LogProvidersAsInformation(Configuration);
            logger.LogConfigurationValuesAsInformation(Configuration);
            logger.LogConfigurationKeySourceAsInformation(Configuration, "ExtenalSystem:AccessToken"); // This is deliberately misspelled.
            logger.LogAllConnectionStringsAsInformation(Configuration);
            //
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Example v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}