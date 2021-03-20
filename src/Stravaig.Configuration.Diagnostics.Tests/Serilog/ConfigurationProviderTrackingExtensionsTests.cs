using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Serilog.Events;
using Shouldly;
using Stravaig.Configuration.Diagnostics;
using Stravaig.Configuration.Diagnostics.Serilog;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__data;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__Extensions;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Serilog
{
    [TestFixture]
    public class ConfigurationProviderTrackingExtensionsTests : SerilogTestBase
    {
        private const string KeyName = "SomeSection:SomeKey";
        
        [Test]
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void NoProviders_LogsNoProviders(LogEventLevel level)
        {
            SetupConfig(_ => { });
            SetupLogger();

            Logger.LogConfigurationKeySource(level, ConfigRoot, KeyName);

            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var log = logs[0];
            
            log.GetLevel().ShouldBe(level);
            log.GetMessage().ShouldContain($"Cannot track \"{KeyName}\". No configuration providers found.");
            log.GetMessage().Split(Environment.NewLine).ShouldNotContain(string.Empty);
        }
        
        [Test]
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void KeyNotFoundInAnyProviderWithCompressedTrue_LogsNotFound(LogEventLevel level)
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
            
            log.GetLevel().ShouldBe(level);
            log.GetMessage().ShouldContain($"Provider sources for value of configuration key \"{KeyName}\" were not found.");
            log.GetMessage().Split(Environment.NewLine).ShouldNotContain(string.Empty);
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void KeyNotFoundInAnyProviderWithCompressedFalse_LogsNotFound(LogEventLevel level)
        {
            SetupConfig(b =>
            {
                b.AddInMemoryCollection();
                b.AddInMemoryCollection();
            });
            SetupLogger();

            Logger.LogConfigurationKeySource(level, ConfigRoot, KeyName);

            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var log = logs[0];
            
            log.GetLevel().ShouldBe(level);
            log.GetMessage().Count(c => c == '*').ShouldBe(2);
            log.GetMessage().ShouldEndWith($"Key not found in any provider.");
            log.GetMessage().Split(Environment.NewLine).ShouldNotContain(string.Empty);
        }
        
        [Test]
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void SingleProvider_LogsSingleElement(LogEventLevel level)
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
            
            log.GetLevel().ShouldBe(level);
            log.GetMessage().ShouldContain(KeyName);
            log.GetMessage().ShouldContain("SomeValue");
            log.GetMessage().ShouldContain("*");
            log.GetMessage().Count(c => c == '*').ShouldBe(1);
            log.GetMessage().Split(Environment.NewLine).ShouldNotContain(string.Empty);
        }
        
        [Test]
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void ObfuscateValueWhenSecret(LogEventLevel level)
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
            
            log.GetLevel().ShouldBe(level);
            log.GetMessage().ShouldContain(KeyName);
            log.GetMessage().ShouldContain("*");
            log.GetMessage().Count(c => c == '*').ShouldBe(1);
            log.GetMessage().ShouldNotContain("SomeSecretValue");
            log.GetMessage().ShouldContain("REDACTED");
            log.GetMessage().Split(Environment.NewLine).ShouldNotContain(string.Empty);
        }
        
        [Test]
        [TestCaseSource(typeof(LogEventLevelSource))]
        public void TwoProviders_LogsBothProviders(LogEventLevel level)
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
            
            log.GetLevel().ShouldBe(level);
            log.GetMessage().ShouldContain(KeyName);
            log.GetMessage().ShouldContain("SomeValue");
            log.GetMessage().ShouldContain("*");
            log.GetMessage().Count(c => c == '*').ShouldBe(2);
            log.GetMessage().ShouldContain("\"MemoryConfigurationProvider\" ==> \"SomeValue\"");
            log.GetMessage().ShouldContain("\"JsonStreamConfigurationProvider\" ==> \"SomeNewValue\"");
            log.GetMessage().Split(Environment.NewLine).ShouldNotContain(string.Empty);
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void ThreeProviders_TwoWithValues_LogsThreeProvidersWithNullForMissingValue(LogEventLevel level)
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
            
            log.GetLevel().ShouldBe(level);
            var message = log.GetMessage();
            Console.WriteLine(message);
            message.ShouldContain(KeyName);
            message.ShouldContain("SomeValue");
            message.ShouldContain("*");
            message.Count(c => c == '*').ShouldBe(3);
            message.ShouldContain("\"MemoryConfigurationProvider\" ==> \"SomeValue\"");
            message.ShouldContain("\"MemoryConfigurationProvider\" ==> null");
            message.ShouldContain("\"JsonStreamConfigurationProvider\" ==> \"SomeNewValue\"");
            message.Split(Environment.NewLine).ShouldNotContain(string.Empty);
        }
        
        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void ThreeProviders_TwoWithValues_LogsTwoProvidersWithCompressedOption(LogEventLevel level)
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
            
            log.GetLevel().ShouldBe(level);
            var message = log.GetMessage();
            Console.WriteLine(message);
            message.ShouldContain(KeyName);
            message.ShouldContain("SomeValue");
            message.ShouldContain("*");
            message.Count(c => c == '*').ShouldBe(2);
            message.ShouldContain("\"MemoryConfigurationProvider\" ==> \"SomeValue\"");
            message.ShouldNotContain("\"MemoryConfigurationProvider\" ==> null");
            message.ShouldContain("\"JsonStreamConfigurationProvider\" ==> \"SomeNewValue\"");
            message.Split(Environment.NewLine).ShouldNotContain(string.Empty);
        }

        [Test]
        public void CheckInformationSpecificMethod()
        {
            SetupConfig();
            SetupLogger();
            
            Logger.LogConfigurationKeySourceAsInformation(ConfigRoot, KeyName);
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].GetLevel().ShouldBe(LogEventLevel.Information);
        }

        [Test]
        public void CheckDebugSpecificMethod()
        {
            SetupConfig();
            SetupLogger();
            
            Logger.LogConfigurationKeySourceAsDebug(ConfigRoot, KeyName);
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].GetLevel().ShouldBe(LogEventLevel.Debug);
        }

        [Test]
        public void CheckTraceSpecificMethod()
        {
            SetupConfig();
            SetupLogger();
            
            Logger.LogConfigurationKeySourceAsVerbose(ConfigRoot, KeyName);
            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].GetLevel().ShouldBe(LogEventLevel.Verbose);
        }
    }
}