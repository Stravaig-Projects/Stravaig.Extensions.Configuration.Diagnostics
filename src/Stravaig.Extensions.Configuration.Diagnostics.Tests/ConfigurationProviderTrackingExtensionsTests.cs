using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__data;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests
{
    [TestFixture]
    public class ConfigurationProviderTrackingExtensionsTests : TestBase
    {
        private const string KeyName = "SomeSection:SomeKey";
        
        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void NoProviders_LogsNoProviders(LogLevel level)
        {
            SetupConfig(b => { });
            SetupLogger();

            Logger.LogConfigurationKeySource(level, ConfigRoot, KeyName);

            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var log = logs[0];
            
            log.LogLevel.ShouldBe(level);
            log.FormattedMessage.ShouldContain($"Cannot track {KeyName}. No configuration providers found.");
            log.FormattedMessage.Split(Environment.NewLine).ShouldNotContain(string.Empty);
        }
        
        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void KeyNotFoundInAnyProviderWithCompressedTrue_LogsNotFound(LogLevel level)
        {
            SetupConfig(b =>
            {
                b.AddInMemoryCollection();
                b.AddInMemoryCollection();
            });
            SetupLogger();

            Logger.LogConfigurationKeySource(level, ConfigRoot, KeyName, true);

            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var log = logs[0];
            
            log.LogLevel.ShouldBe(level);
            log.FormattedMessage.ShouldContain($"Provider sources for value of {KeyName} were not found.");
            log.FormattedMessage.Split(Environment.NewLine).ShouldNotContain(string.Empty);
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void KeyNotFoundInAnyProviderWithCompressedFalse_LogsNotFound(LogLevel level)
        {
            SetupConfig(b =>
            {
                b.AddInMemoryCollection();
                b.AddInMemoryCollection();
            });
            SetupLogger();

            Logger.LogConfigurationKeySource(level, ConfigRoot, KeyName, false);

            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var log = logs[0];
            
            log.LogLevel.ShouldBe(level);
            log.FormattedMessage.Count(c => c == '*').ShouldBe(2);
            log.FormattedMessage.ShouldContain($"{KeyName} not found in any provider.");
            log.FormattedMessage.Split(Environment.NewLine).ShouldNotContain(string.Empty);
        }
        
        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void SingleProvider_LogsSingleElement(LogLevel level)
        {
            SetupConfig(builder =>
            {
                builder.AddInMemoryCollection(new Dictionary<string, string>
                {
                    {KeyName, "SomeValue"}
                });
            });
            SetupLogger();

            Logger.LogConfigurationKeySource(level, ConfigRoot, "SomeSection:SomeKey");

            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var log = logs[0];
            
            log.LogLevel.ShouldBe(level);
            log.FormattedMessage.ShouldContain(KeyName);
            log.FormattedMessage.ShouldContain("SomeValue");
            log.FormattedMessage.ShouldContain("*");
            log.FormattedMessage.Count(c => c == '*').ShouldBe(1);
            log.FormattedMessage.Split(Environment.NewLine).ShouldNotContain(string.Empty);
        }
        
        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void ObfuscateValueWhenSecret(LogLevel level)
        {
            SetupConfig(builder =>
            {
                builder.AddInMemoryCollection(new Dictionary<string, string>
                {
                    {KeyName, "SomeSecretValue"}
                });
            });
            SetupLogger();
            ConfigurationDiagnosticsOptions options = ConfigurationDiagnosticsOptions.SetupBy
                .Obfuscating.ByRedacting()
                .And.MatchingConfigurationKeys.Containing(KeyName)
                .AndFinally.BuildOptions();

            Logger.LogConfigurationKeySource(level, ConfigRoot, "SomeSection:SomeKey", false, options);

            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var log = logs[0];
            
            log.LogLevel.ShouldBe(level);
            log.FormattedMessage.ShouldContain(KeyName);
            log.FormattedMessage.ShouldContain("*");
            log.FormattedMessage.Count(c => c == '*').ShouldBe(1);
            log.FormattedMessage.ShouldNotContain("SomeSecretValue");
            log.FormattedMessage.ShouldContain("REDACTED");
            log.FormattedMessage.Split(Environment.NewLine).ShouldNotContain(string.Empty);
        }
        
        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void TwoProviders_LogsBothProviders(LogLevel level)
        {
            SetupConfig(builder =>
            {
                builder.AddInMemoryCollection(new Dictionary<string, string>
                {
                    {KeyName, "SomeValue"}
                });
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream, Encoding.UTF8);
                writer.WriteLine(@"
{
  ""SomeSection"" : {
    ""SomeKey"" : ""SomeNewValue""
  }
}
");
                writer.Flush();
                stream.Position = 0;
                builder.AddJsonStream(stream);
            });
            SetupLogger();

            Logger.LogConfigurationKeySource(level, ConfigRoot, "SomeSection:SomeKey");

            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var log = logs[0];
            
            log.LogLevel.ShouldBe(level);
            log.FormattedMessage.ShouldContain(KeyName);
            log.FormattedMessage.ShouldContain("SomeValue");
            log.FormattedMessage.ShouldContain("*");
            log.FormattedMessage.Count(c => c == '*').ShouldBe(2);
            log.FormattedMessage.ShouldContain("MemoryConfigurationProvider ==> \"SomeValue\"");
            log.FormattedMessage.ShouldContain("JsonStreamConfigurationProvider ==> \"SomeNewValue\"");
            log.FormattedMessage.Split(Environment.NewLine).ShouldNotContain(string.Empty);
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void ThreeProviders_TwoWithValues_LogsThreeProvidersWithNullForMissingValue(LogLevel level)
        {
            SetupConfig(builder =>
            {
                builder.AddInMemoryCollection(new Dictionary<string, string>
                {
                    {KeyName, "SomeValue"}
                });
                builder.AddInMemoryCollection();
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream, Encoding.UTF8);
                writer.WriteLine(@"
{
  ""SomeSection"" : {
    ""SomeKey"" : ""SomeNewValue""
  }
}
");
                writer.Flush();
                stream.Position = 0;
                builder.AddJsonStream(stream);
            });
            SetupLogger();

            Logger.LogConfigurationKeySource(level, ConfigRoot, "SomeSection:SomeKey");

            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var log = logs[0];
            
            log.LogLevel.ShouldBe(level);
            log.FormattedMessage.ShouldContain(KeyName);
            log.FormattedMessage.ShouldContain("SomeValue");
            log.FormattedMessage.ShouldContain("*");
            log.FormattedMessage.Count(c => c == '*').ShouldBe(3);
            log.FormattedMessage.ShouldContain("MemoryConfigurationProvider ==> \"SomeValue\"");
            log.FormattedMessage.ShouldContain("MemoryConfigurationProvider ==> null");
            log.FormattedMessage.ShouldContain("JsonStreamConfigurationProvider ==> \"SomeNewValue\"");
            log.FormattedMessage.Split(Environment.NewLine).ShouldNotContain(string.Empty);
        }
        
        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void ThreeProviders_TwoWithValues_LogsTwoProvidersWithCompressedOption(LogLevel level)
        {
            SetupConfig(builder =>
            {
                builder.AddInMemoryCollection(new Dictionary<string, string>
                {
                    {KeyName, "SomeValue"}
                });
                builder.AddInMemoryCollection();
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream, Encoding.UTF8);
                writer.WriteLine(@"
{
  ""SomeSection"" : {
    ""SomeKey"" : ""SomeNewValue""
  }
}
");
                writer.Flush();
                stream.Position = 0;
                builder.AddJsonStream(stream);
            });
            SetupLogger();

            Logger.LogConfigurationKeySource(level, ConfigRoot, "SomeSection:SomeKey", true);

            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var log = logs[0];
            
            log.LogLevel.ShouldBe(level);
            log.FormattedMessage.ShouldContain(KeyName);
            log.FormattedMessage.ShouldContain("SomeValue");
            log.FormattedMessage.ShouldContain("*");
            log.FormattedMessage.Count(c => c == '*').ShouldBe(2);
            log.FormattedMessage.ShouldContain("MemoryConfigurationProvider ==> \"SomeValue\"");
            log.FormattedMessage.ShouldNotContain("MemoryConfigurationProvider ==> null");
            log.FormattedMessage.ShouldContain("JsonStreamConfigurationProvider ==> \"SomeNewValue\"");
            log.FormattedMessage.Split(Environment.NewLine).ShouldNotContain(string.Empty);
        }

        [Test]
        public void CheckInformationSpecificMethod()
        {
            SetupConfig(b => { });
            SetupLogger();
            
            Logger.LogConfigurationKeySourceAsInformation(ConfigRoot, KeyName);
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].LogLevel.ShouldBe(LogLevel.Information);
        }

        [Test]
        public void CheckDebugSpecificMethod()
        {
            SetupConfig(b => { });
            SetupLogger();
            
            Logger.LogConfigurationKeySourceAsDebug(ConfigRoot, KeyName);
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].LogLevel.ShouldBe(LogLevel.Debug);
        }

        [Test]
        public void CheckTraceSpecificMethod()
        {
            SetupConfig(b => { });
            SetupLogger();
            
            Logger.LogConfigurationKeySourceAsTrace(ConfigRoot, KeyName);
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].LogLevel.ShouldBe(LogLevel.Trace);
        }
    }
}