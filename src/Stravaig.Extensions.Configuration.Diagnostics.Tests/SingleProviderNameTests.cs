using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__data;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests
{
    public class SingleProviderNameTests : TestBase
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
            var logs = CaptureLoggerProvider.GetLogEntriesFor<SingleProviderNameTests>();
            logs.Count.ShouldBe(1);
            logs[0].LogLevel.ShouldBe(LogLevel.Information);
            logs[0].FormattedMessage.ShouldContain(typeof(MemoryConfigurationProvider).FullName);
        }
        
        [Test]
        public void LogProviderToDebug()
        {
            // Act
            Logger.LogProviderNamesAsDebug(ConfigRoot);

            // Assert
            var logs = CaptureLoggerProvider.GetLogEntriesFor<SingleProviderNameTests>();
            logs.Count.ShouldBe(1);
            logs[0].LogLevel.ShouldBe(LogLevel.Debug);
            logs[0].FormattedMessage.ShouldContain(typeof(MemoryConfigurationProvider).FullName);
        }

        [Test]
        public void LogProviderToTrace()
        {
            // Act
            Logger.LogProviderNamesAsTrace(ConfigRoot);

            // Assert
            var logs = CaptureLoggerProvider.GetLogEntriesFor<SingleProviderNameTests>();
            logs.Count.ShouldBe(1);
            logs[0].LogLevel.ShouldBe(LogLevel.Trace);
            logs[0].FormattedMessage.ShouldContain(typeof(MemoryConfigurationProvider).FullName);
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void LogProviderToSpecifiedLevel(LogLevel level)
        {
            // Act
            Logger.LogProviderNames(ConfigRoot, level);

            // Assert
            var logs = CaptureLoggerProvider.GetLogEntriesFor<SingleProviderNameTests>();
            logs.Count.ShouldBe(1);
            logs[0].LogLevel.ShouldBe(level);
            logs[0].FormattedMessage.ShouldContain(typeof(MemoryConfigurationProvider).FullName);
        }
    }
}