using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using NUnit.Framework;
using Serilog.Events;
using Shouldly;
using Stravaig.Configuration.Diagnostics.Serilog;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__data;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__Extensions;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Serilog
{
    public class SerilogSingleProviderNameTests : SerilogTestBase
    {
        [SetUp]
        public void Setup()
        {
            SetupLogger();
            SetupConfig(builder =>
                builder.AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"ConfigOne", "ValueOne"},
                }));
        }

        [Test]
        public void LogProviderToInformation()
        {
            // Act
            Logger.LogProviderNamesAsInformation(ConfigRoot);
            
            // Assert
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var entry = logs[0];
            entry.GetLevel().HasValue.ShouldBeTrue();
            entry.GetLevel().Value.ShouldBe(LogEventLevel.Information);
            entry.GetMessage().ShouldContain(typeof(MemoryConfigurationProvider).FullName);
        }
        
        [Test]
        public void LogProviderToDebug()
        {
            // Act
            Logger.LogProviderNamesAsDebug(ConfigRoot);
        
            // Assert
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].GetLevel().ShouldBe(LogEventLevel.Debug);
            logs[0].GetMessage().ShouldContain(typeof(MemoryConfigurationProvider).FullName);
        }
        
        [Test]
        public void LogProviderToTrace()
        {
            // Act
            Logger.LogProviderNamesAsTrace(ConfigRoot);
        
            // Assert
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].GetLevel().ShouldBe(LogEventLevel.Verbose);
            logs[0].GetMessage().ShouldContain(typeof(MemoryConfigurationProvider).FullName);
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
        }
    }
}