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

            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            logs[0].LogLevel.ShouldBe(LogLevel.Information);
            logs[0].FormattedMessage.ShouldStartWith("The following values are available");
            logs[0].FormattedMessage.ShouldContain("ConfigOne : One");
            logs[0].FormattedMessage.ShouldContain("ConfigTwo : Dos");
            logs[0].FormattedMessage.ShouldContain("ConfigThree : Tres");
        }
    }
}