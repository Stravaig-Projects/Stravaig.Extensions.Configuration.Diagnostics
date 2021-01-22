using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Stravaig.Extensions.Configuration.Diagnostics.AdHocTests
{
    public class AllLoggingTypeTests
    {
        private IConfigurationRoot _configurationRoot;
        private IServiceProvider _provider;
        
        [SetUp]
        public void Setup()
        {
            Environment.SetEnvironmentVariable("AllLevels", "4");
            var services = new ServiceCollection();
            services.AddLogging(loggingBuilder => 
                loggingBuilder
                    .AddSeq()
                    .AddConsole());
            _provider = services.BuildServiceProvider();
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(new Dictionary<string, string>
            {
                {"AllLevels", "1"},
                {"SomeLevels", "One"},
                {"ConnectionStrings:Simple", "server=my-server;database=my-database"},
                {"ConnectionStrings:SQLite", "Data Source=:memory:;Version=3;New=True;"},
                {"ConnectionStrings:SqlServer", "Server=myServerName\\myInstanceName;Database=myDataBase;User Id=myUsername;Password=myPassword;"},
                {"ConnectionStrings:Oracle", "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=MyHost)(PORT=MyPort))(CONNECT_DATA=(SERVICE_NAME=MyOracleSID)));User Id=myUsername;Password=myPassword;"},
            });
            builder.AddInMemoryCollection(new Dictionary<string, string>
            {
                {"AllLevels", "2"},
                {"FeatureFlags:FeatureA", "true"},
                {"FeatureFlags:FeatureB", "false"},
                {"FeatureFlags:FeatureC", "true"}
            });
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream, Encoding.UTF8);
            writer.WriteLine(@"
                {
                  ""AllLevels"": 3,
                  ""SomeLevels"": ""Three"",
                  ""SomeSection"" : {
                    ""SomeKey"" : ""SomeNewValue""
                  }
                }
                ");
            writer.Flush();
            stream.Position = 0;
            builder.AddJsonStream(stream);
            builder.AddEnvironmentVariables();
            _configurationRoot = builder.Build();
        }

        [OneTimeTearDown]
        public async Task WaitForFlush()
        {
            _provider.GetRequiredService<ILoggerFactory>()
                .CreateLogger<AllLoggingTypeTests>()
                .LogInformation("Logs are flushed periodically. Waiting...");
            await Task.Delay(5000);
        }
        
        [Test]
        public void RunLogProvidersAsInformation()
        {
            var logger = _provider.GetRequiredService<ILoggerFactory>().CreateLogger<AllLoggingTypeTests>();
            logger.LogProvidersAsInformation(_configurationRoot);
        }
        
        [Test]
        public void RunLogProviderNamesAsInformation()
        {
            var logger = _provider.GetRequiredService<ILoggerFactory>().CreateLogger<AllLoggingTypeTests>();
            logger.LogProviderNamesAsInformation(_configurationRoot);
        }
        
        [Test]
        public void RunLogConfigurationValuesAsInformation()
        {
            var logger = _provider.GetRequiredService<ILoggerFactory>().CreateLogger<AllLoggingTypeTests>();
            logger.LogConfigurationValuesAsInformation(_configurationRoot);
        }
        
        [Test]
        public void RunLogConfigurationKeySourceAsInformationUncompressed()
        {
            var logger = _provider.GetRequiredService<ILoggerFactory>().CreateLogger<AllLoggingTypeTests>();
            logger.LogConfigurationKeySourceAsInformation(_configurationRoot, "AllLevels");
        }

        [Test]
        public void RunLogConfigurationKeySourceAsInformationCompressed()
        {
            var logger = _provider.GetRequiredService<ILoggerFactory>().CreateLogger<AllLoggingTypeTests>();
            logger.LogConfigurationKeySourceAsInformation(_configurationRoot, "SomeLevels", true);
        }
        
        [Test]
        public void RunLogAllConnectionStringsAsInformation()
        {
            var logger = _provider.GetRequiredService<ILoggerFactory>().CreateLogger<AllLoggingTypeTests>();
            logger.LogAllConnectionStringsAsInformation(_configurationRoot);
        }
    }
}