using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Shouldly;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests
{
    public class LogValuesTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            SetupLogger();
            SetupConfig(builder =>
            {
                builder.AddInMemoryCollection(new Dictionary<string, string>
                    {
                        {"ConfigOne", "One"},
                        {"ConfigTwo", "Two"},
                    })
                    .AddInMemoryCollection(new Dictionary<string, string>
                    {
                        {"ConfigTwo", "Dos"},
                        {"ConfigThree", "Tres"}
                    });
            });
        }

        [Test]
        public void LogsThreeValuesAsInformation()
        {
            ConfigRoot.LogConfigurationValuesAsInformation(Logger);
            CheckLog(LogLevel.Information);
        }
        
        [Test]
        public void LogsThreeValuesAsDebug()
        {
            ConfigRoot.LogConfigurationValuesAsDebug(Logger);
            CheckLog(LogLevel.Debug);
        }
        
        [Test]
        public void LogsThreeValuesAsTrace()
        {
            ConfigRoot.LogConfigurationValuesAsTrace(Logger);
            CheckLog(LogLevel.Trace);
        }

        [Test]
        [TestCase(LogLevel.Critical)]
        [TestCase(LogLevel.Error)]
        [TestCase(LogLevel.Warning)]
        [TestCase(LogLevel.Information)]
        [TestCase(LogLevel.Debug)]
        [TestCase(LogLevel.Trace)]
        public void LogsThreeValuesAsSpecificLogLevel(LogLevel level)
        {
            ConfigRoot.LogConfigurationValues(Logger, level);

            CheckLog(level);
        }

        private void CheckLog(LogLevel level)
        {
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].LogLevel.ShouldBe(level);
            var message = logs[0].FormattedMessage;
            message.ShouldStartWith("The following values are available");
            message.ShouldContain("ConfigOne : One");
            message.ShouldContain("ConfigTwo : Dos");
            message.ShouldContain("ConfigThree : Tres");
        }

    }
}