using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics.Logging;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.RegressionTests
{
    [TestFixture]
    public class DotsBugLoggerExtensionTests : LoggerExtensionsTestBase
    {
        [Test]
        public void ConfigElementsWithDotsShouldRenderPropertiesInMessage()
        {
            SetupConfig(c =>
            {
                c.AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string>("ItemA", "Alpha"),
                    new KeyValuePair<string, string>("ItemB", "Beta"),
                    new KeyValuePair<string, string>("ItemB.A", "Beta-Alpha"),
                    new KeyValuePair<string, string>("ItemC", "Gamma"),
                });
            });
            SetupLogger();

            Logger.LogConfigurationValuesAsInformation(ConfigRoot);

            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var log = logs[0];
            log.FormattedMessage.ShouldContain("ItemC : Gamma");
            log.FormattedMessage.ShouldContain("ItemB.A : Beta-Alpha");
            log.FormattedMessage.ShouldContain("ItemB : Beta");
            log.FormattedMessage.ShouldContain("ItemA : Alpha");
        }
    }
}