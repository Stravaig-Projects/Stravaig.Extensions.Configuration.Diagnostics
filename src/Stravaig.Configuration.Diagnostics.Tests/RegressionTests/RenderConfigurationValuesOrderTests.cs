using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics.Logging;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.RegressionTests
{
    [TestFixture]
    public class RenderConfigurationValuesOrderTests : LoggerExtensionsTestBase
    {
        [Test]
        public void EnsureOrderOfConfigValuesIsAscendingByKey()
        {
            SetupConfig(c =>
            {
                c.AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string>("ItemA", "One"),
                    new KeyValuePair<string, string>("ItemB:1", "Two"),
                    new KeyValuePair<string, string>("ItemB:2", "Three"),
                    new KeyValuePair<string, string>("ItemC", "Four"),
                });
            });
            SetupLogger();

            Logger.LogConfigurationValuesAsInformation(ConfigRoot);

            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var log = logs[0];
            var logLines = log.FormattedMessage
                .Split(Environment.NewLine)
                .Where(l => l.StartsWith("Item"))
                .ToArray();

            for (int i = 0; i < logLines.Length; i++)
                Console.WriteLine($"logLines[{i}] = \"{logLines[i]}\"");
            
            logLines[0].ShouldBe("ItemA : One");
            logLines[1].ShouldBe("ItemB : (null)");
            logLines[2].ShouldBe("ItemB:1 : Two");
            logLines[3].ShouldBe("ItemB:2 : Three");
            logLines[4].ShouldBe("ItemC : Four");

        }
    }
}