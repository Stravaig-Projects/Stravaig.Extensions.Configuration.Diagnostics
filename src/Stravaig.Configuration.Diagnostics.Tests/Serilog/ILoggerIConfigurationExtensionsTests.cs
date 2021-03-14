using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Serilog.Events;
using Shouldly;
using Stravaig.Configuration.Diagnostics;
using Stravaig.Configuration.Diagnostics.Obfuscators;
using Stravaig.Configuration.Diagnostics.Serilog;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__data;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__Extensions;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Serilog
{
    // ReSharper disable once InconsistentNaming
    public class ILoggerIConfigurationExtensionsTests : SerilogTestBase
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
            Logger.LogConfigurationValuesAsInformation(ConfigRoot);
            CheckLog(LogEventLevel.Information);
        }
        
        [Test]
        public void LogsThreeValuesWithObfuscationAsInformation()
        {
            var options = SetupOptions();
            Logger.LogConfigurationValuesAsInformation(ConfigRoot, options);
            CheckObfuscatedLog(LogEventLevel.Information);
        }
        
        [Test]
        public void LogsThreeValuesAsDebug()
        {
            Logger.LogConfigurationValuesAsDebug(ConfigRoot);
            CheckLog(LogEventLevel.Debug);
        }
        
        [Test]
        public void LogsThreeValuesWithObfuscationAsDebug()
        {
            var options = SetupOptions();
            Logger.LogConfigurationValuesAsDebug(ConfigRoot, options);
            CheckObfuscatedLog(LogEventLevel.Debug);
        }

        [Test]
        public void LogsThreeValuesAsVerbose()
        {
            Logger.LogConfigurationValuesAsVerbose(ConfigRoot);
            CheckLog(LogEventLevel.Verbose);
        }

        [Test]
        public void LogsThreeValuesWithObfuscationAsTrace()
        {
            var options = SetupOptions();
            Logger.LogConfigurationValuesAsVerbose(ConfigRoot, options);
            CheckObfuscatedLog(LogEventLevel.Verbose);
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void LogsThreeValuesAsSpecificLogLevel(LogEventLevel level)
        {
            Logger.LogConfigurationValues(ConfigRoot, level);
            CheckLog(level);
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void LoggedSecretsAreObfuscated(LogEventLevel level)
        {
            var options = SetupOptions();
            Logger.LogConfigurationValues(ConfigRoot, level, options);
            CheckObfuscatedLog(level);
        }

        private static ConfigurationDiagnosticsOptions SetupOptions()
        {
            var options = new ConfigurationDiagnosticsOptions();
            options.BuildConfigurationKeyMatcher(builder => builder.AddContainsMatcher("ConfigThree"));
            options.Obfuscator = new FixedAsteriskObfuscator();
            return options;
        }

        private void CheckLog(LogEventLevel level)
        {
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].GetLevel().ShouldBe(level);
            var message = logs[0].GetMessage();
            message.ShouldStartWith("The following values are available");
            message.ShouldContain("ConfigOne : \"One\"");
            message.ShouldContain("ConfigTwo : \"Dos\"");
            message.ShouldContain("ConfigThree : \"Tres\"");
        }
        
        private void CheckObfuscatedLog(LogEventLevel level)
        {
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].GetLevel().ShouldBe(level);
            var message = logs[0].GetMessage();
            message.ShouldStartWith("The following values are available");
            message.ShouldContain("ConfigOne : \"One\"");
            message.ShouldContain("ConfigTwo : \"Dos\"");
            message.ShouldContain("ConfigThree : \"****\"");
        }
    }
}