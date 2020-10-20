using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Shouldly;

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
            ConfigRoot.LogProviderNamesAsInformation(Logger);

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
            ConfigRoot.LogProviderNamesAsDebug(Logger);

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
            ConfigRoot.LogProviderNamesAsTrace(Logger);

            // Assert
            var logs = CaptureLoggerProvider.GetLogEntriesFor<SingleProviderNameTests>();
            logs.Count.ShouldBe(1);
            logs[0].LogLevel.ShouldBe(LogLevel.Trace);
            logs[0].FormattedMessage.ShouldContain(typeof(MemoryConfigurationProvider).FullName);
        }

        [Test]
        [TestCase(LogLevel.Critical)]
        [TestCase(LogLevel.Error)]
        [TestCase(LogLevel.Warning)]
        [TestCase(LogLevel.Information)]
        [TestCase(LogLevel.Debug)]
        [TestCase(LogLevel.Trace)]
        public void LogProviderToSpecifiedLevel(LogLevel level)
        {
            // Act
            ConfigRoot.LogProviderNames(Logger, level);

            // Assert
            var logs = CaptureLoggerProvider.GetLogEntriesFor<SingleProviderNameTests>();
            logs.Count.ShouldBe(1);
            logs[0].LogLevel.ShouldBe(level);
            logs[0].FormattedMessage.ShouldContain(typeof(MemoryConfigurationProvider).FullName);
        }
    }
}