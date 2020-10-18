using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Logging.Diagnostics;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests
{
    public class SingleProviderTests
    {
        private TestCaptureLoggerProvider _captureLoggerProvider;
        private ILogger _logger;
        private IConfigurationRoot _configRoot;
        [SetUp]
        public void Setup()
        {
            _captureLoggerProvider = new TestCaptureLoggerProvider();
            var loggerFactory = new ServiceCollection()
                .AddLogging(b =>
                {
                    b.AddConsole(o =>
                    {
                        o.DisableColors = true;
                    });
                    b.AddProvider(_captureLoggerProvider);
                    b.SetMinimumLevel(LogLevel.Trace);
                })
                .BuildServiceProvider()
                .GetService<ILoggerFactory>();
            _logger = loggerFactory.CreateLogger<SingleProviderTests>();

            _configRoot = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"ConfigOne", "ValueOne"},
                })
                .Build();
        }

        [Test]
        public void LogProviderToInformation()
        {
            // Act
            _configRoot.LogProviderNamesAsInformation(_logger);

            // Assert
            var logs = _captureLoggerProvider.GetLogEntriesFor<SingleProviderTests>();
            logs.Count.ShouldBe(1);
            logs[0].LogLevel.ShouldBe(LogLevel.Information);
            logs[0].FormattedMessage.ShouldContain(typeof(MemoryConfigurationProvider).FullName);
        }
        
        [Test]
        public void LogProviderToDebug()
        {
            // Act
            _configRoot.LogProviderNamesAsDebug(_logger);

            // Assert
            var logs = _captureLoggerProvider.GetLogEntriesFor<SingleProviderTests>();
            logs.Count.ShouldBe(1);
            logs[0].LogLevel.ShouldBe(LogLevel.Debug);
            logs[0].FormattedMessage.ShouldContain(typeof(MemoryConfigurationProvider).FullName);
        }

        [Test]
        public void LogProviderToTrace()
        {
            // Act
            _configRoot.LogProviderNamesAsTrace(_logger);

            // Assert
            var logs = _captureLoggerProvider.GetLogEntriesFor<SingleProviderTests>();
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
            _configRoot.LogProviderNames(_logger, level);

            // Assert
            var logs = _captureLoggerProvider.GetLogEntriesFor<SingleProviderTests>();
            logs.Count.ShouldBe(1);
            logs[0].LogLevel.ShouldBe(level);
            logs[0].FormattedMessage.ShouldContain(typeof(MemoryConfigurationProvider).FullName);
        }
    }
}