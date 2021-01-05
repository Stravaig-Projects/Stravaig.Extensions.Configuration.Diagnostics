using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Stravaig.Extensions.Configuration.Diagnostics;

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
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Example", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var logFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
            var logger = logFactory.CreateLogger<Startup>();
            ConfigurationDiagnosticsOptions.SetupBy
                .Obfuscating.WithAsterisks()
                .And.MatchingConfigurationKeys.Where
                .KeyContains("AccessToken")
                .OrContains("ConnectionString")
                .And.MatchingConnectionStringKeys
                .Containing("Password")
                .AndFinally.ApplyOptions(ConfigurationDiagnosticsOptions.GlobalOptions);
            logger.LogProvidersAsInformation(Configuration);
            logger.LogConfigurationValuesAsInformation(Configuration);
            logger.LogConfigurationKeySourceAsInformation(Configuration, "ExtenalSystem:AccessToken");
            logger.LogAllConnectionStringsAsInformation(Configuration);
            
            
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