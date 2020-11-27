using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Serilog;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__data;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests
{
    [TestFixture]
    public class ConfigurationProviderTrackingTests : TestBase
    {
        private const string KeyName = "SomeSection:SomeKey";
        
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
        }

        [Test]
        [TestCaseSource(typeof(LogLevelSource))]
        public void ThreeProviders_Two_WithValues_LogsThreeProvidersWithNullForMissingValue(LogLevel level)
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
        }
    }
}