using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Serilog;
using Serilog.Formatting.Json;
using Shouldly;
using Stravaig.Configuration.Diagnostics.Logging;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.RegressionTests
{
    /// <summary>
    /// This tests Issue #31
    /// https://github.com/Stravaig-Projects/Stravaig.Extensions.Configuration.Diagnostics/issues/31
    /// </summary>
    [TestFixture]
    public class ConnectionStringLoggingWithSerilogTests
    {
        [Test]
        public void KeyValuesRenderedInMessageCorrectly()
        {
            var writer = new StringWriter();
            var logger = SetupLogger(writer);
            var config = SetupConfig();
            
            logger.LogConnectionStringAsInformation(config, "Simple");

            var logText = writer.GetStringBuilder().ToString();
            Console.WriteLine(logText);
            logText.ShouldNotBeNullOrWhiteSpace();

            var logObject = JObject.Parse(logText);
            var renderedMessage = logObject["RenderedMessage"];
            Console.WriteLine(renderedMessage);

            
            var properties = (JObject) logObject["Properties"];
            properties.ShouldContainKey("Simple_database");
            properties.ShouldContainKey("Simple_server");
        }

        private static ILogger<ConnectionStringLoggingWithSerilogTests> SetupLogger(StringWriter writer)
        {
            var configuredLogger = new LoggerConfiguration()
                .WriteTo.TextWriter(new JsonFormatter(renderMessage:true), writer)
                .CreateLogger();
            var services = new ServiceCollection();
            services.AddLogging(loggingBuilder => loggingBuilder
                .AddSerilog(
                    logger: configuredLogger,
                    dispose: true));
            var provider = services.BuildServiceProvider();
            var factory = provider.GetRequiredService<ILoggerFactory>();
            var logger = factory.CreateLogger<ConnectionStringLoggingWithSerilogTests>();
            return logger;
        }
        
        protected IConfigurationRoot SetupConfig()
        {
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(new Dictionary<string, string>
            {
                {"ConnectionStrings:Simple", "server=my-server;database=my-database"}
            });
            return builder.Build();
        }
    }
}