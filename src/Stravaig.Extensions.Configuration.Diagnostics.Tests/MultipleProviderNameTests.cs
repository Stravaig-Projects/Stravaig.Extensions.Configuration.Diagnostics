using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.CommandLine;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__data;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests
{
    [TestFixture]
    public class MultipleProviderNameTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            SetupLogger();
            SetupConfig(builder =>
            {
                builder.AddInMemoryCollection(Array.Empty<KeyValuePair<string, string>>())
                    .AddJsonFile("appsettings.json", true)
                    .AddEnvironmentVariables()
                    .AddCommandLine(Array.Empty<string>());
            });
        }
        
        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void LogProviderToSpecifiedLevel(LogLevel level)
        {
            // Act
            ConfigRoot.LogProviderNames(Logger, level);

            // Assert
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].LogLevel.ShouldBe(level);
            logs[0].FormattedMessage.ShouldContain(typeof(MemoryConfigurationProvider).FullName);
            logs[0].FormattedMessage.ShouldContain(typeof(JsonConfigurationProvider).FullName);
            logs[0].FormattedMessage.ShouldContain(typeof(CommandLineConfigurationProvider).FullName);
            logs[0].FormattedMessage.ShouldContain(typeof(EnvironmentVariablesConfigurationProvider).FullName);
        }
    }
}