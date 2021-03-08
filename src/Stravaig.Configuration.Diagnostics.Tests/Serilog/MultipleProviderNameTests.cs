using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.CommandLine;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.Memory;
using NUnit.Framework;
using Serilog.Events;
using Shouldly;
using Stravaig.Configuration.Diagnostics.Serilog;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__data;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__Extensions;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Serilog
{
    [TestFixture]
    public class MultipleProviderNameTests : SerilogTestBase
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
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void LogProviderToSpecifiedLevel(LogEventLevel level)
        {
            // Act
            Logger.LogProviderNames(ConfigRoot, level);

            // Assert
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].GetLevel().ShouldBe(level);
            logs[0].GetMessage().ShouldContain(typeof(MemoryConfigurationProvider).FullName);
            logs[0].GetMessage().ShouldContain(typeof(JsonConfigurationProvider).FullName);
            logs[0].GetMessage().ShouldContain(typeof(CommandLineConfigurationProvider).FullName);
            logs[0].GetMessage().ShouldContain(typeof(EnvironmentVariablesConfigurationProvider).FullName);
        }
    }
}