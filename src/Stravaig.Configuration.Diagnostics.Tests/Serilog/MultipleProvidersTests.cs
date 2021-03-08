using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Serilog.Events;
using Shouldly;
using Stravaig.Configuration.Diagnostics.Serilog;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__data;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__Extensions;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Serilog
{
    public class MultipleProvidersTests : SerilogTestBase
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
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void LogProviderToSpecifiedLevel(LogEventLevel level)
        {
            // Act
            Logger.LogProviders(ConfigRoot, level);

            // Assert
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var entry = logs[0];
            entry.GetLevel().ShouldBe(level);
            entry.GetMessage().ShouldContain("MemoryConfigurationProvider");
            entry.GetMessage().ShouldContain("JsonConfigurationProvider for 'appsettings.json' (Optional)");
            entry.GetMessage().ShouldContain("JsonConfigurationProvider for 'appsettings.test.json' (Optional)");
            entry.GetMessage().ShouldContain("EnvironmentVariablesConfigurationProvider");
            entry.GetMessage().ShouldContain("CommandLineConfigurationProvider");
        }
    }
}