using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Obfuscators;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__data;

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
        public void LogsThreeValuesWithObfuscationAsInformation()
        {
            var options = SetupOptions();
            ConfigRoot.LogConfigurationValuesAsInformation(Logger, options);
            CheckObfuscatedLog(LogLevel.Information);
        }
        
        [Test]
        public void LogsThreeValuesAsDebug()
        {
            ConfigRoot.LogConfigurationValuesAsDebug(Logger);
            CheckLog(LogLevel.Debug);
        }
        
        [Test]
        public void LogsThreeValuesWithObfuscationAsDebug()
        {
            var options = SetupOptions();
            ConfigRoot.LogConfigurationValuesAsDebug(Logger, options);
            CheckObfuscatedLog(LogLevel.Debug);
        }

        [Test]
        public void LogsThreeValuesAsTrace()
        {
            ConfigRoot.LogConfigurationValuesAsTrace(Logger);
            CheckLog(LogLevel.Trace);
        }

        [Test]
        public void LogsThreeValuesWithObfuscationAsTrace()
        {
            var options = SetupOptions();
            ConfigRoot.LogConfigurationValuesAsTrace(Logger, options);
            CheckObfuscatedLog(LogLevel.Trace);
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void LogsThreeValuesAsSpecificLogLevel(LogLevel level)
        {
            ConfigRoot.LogConfigurationValues(Logger, level);
            CheckLog(level);
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void LoggedSecretsAreObfuscated(LogLevel level)
        {
            var options = SetupOptions();
            ConfigRoot.LogConfigurationValues(Logger, level, options);
            CheckObfuscatedLog(level);
        }

        private static ConfigurationDiagnosticsOptions SetupOptions()
        {
            var options = new ConfigurationDiagnosticsOptions();
            options.BuildConfigurationKeyMatcher(builder => builder.AddContainsMatcher("ConfigThree"));
            options.Obfuscator = new FixedAsteriskObfuscator();
            return options;
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
        
        private void CheckObfuscatedLog(LogLevel level)
        {
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].LogLevel.ShouldBe(level);
            var message = logs[0].FormattedMessage;
            message.ShouldStartWith("The following values are available");
            message.ShouldContain("ConfigOne : One");
            message.ShouldContain("ConfigTwo : Dos");
            message.ShouldContain("ConfigThree : ****");
        }

    }
}