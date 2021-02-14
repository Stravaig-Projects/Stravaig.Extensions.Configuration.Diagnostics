using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics.Logging;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__data;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests
{
    public class MultipleProvidersTests : LoggerExtensionsBase
    {
        [SetUp]
        public void SetUp()
        {
            SetupLogger();
            SetupConfig(builder =>
            {
                builder.AddInMemoryCollection(Array.Empty<KeyValuePair<string, string>>())
                    .AddJsonFile("appsettings.json", true)
                    .AddJsonFile("appsettings.test.json", true)
                    .AddEnvironmentVariables()
                    .AddCommandLine(Array.Empty<string>());
            });
        }
        
        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void LogProviderToSpecifiedLevel(LogLevel level)
        {
            // Act
            Logger.LogProviders(ConfigRoot, level);

            // Assert
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].LogLevel.ShouldBe(level);
            logs[0].FormattedMessage.ShouldContain("MemoryConfigurationProvider");
            logs[0].FormattedMessage.ShouldContain("JsonConfigurationProvider for 'appsettings.json' (Optional)");
            logs[0].FormattedMessage.ShouldContain("JsonConfigurationProvider for 'appsettings.test.json' (Optional)");
            logs[0].FormattedMessage.ShouldContain("EnvironmentVariablesConfigurationProvider");
            logs[0].FormattedMessage.ShouldContain("CommandLineConfigurationProvider");
        }
    }
}