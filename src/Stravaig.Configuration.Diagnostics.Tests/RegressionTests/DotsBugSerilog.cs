using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics.Serilog;
using Stravaig.Extensions.Configuration.Diagnostics.Tests.__Extensions;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.RegressionTests
{
    [TestFixture]
    public class DotsBugSerilog : SerilogTestBase
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
            SetupLogger(true);

            Logger.LogConfigurationValuesAsInformation(ConfigRoot);

            var logs = GetLogs();
            logs.Count.ShouldBe(1);
            var log = logs[0];
            
            Console.WriteLine(log.GetMessage());
            Console.WriteLine(log.GetMessageTemplate());            
            
            log.GetMessage().ShouldContain("ItemC : \"Gamma\"");
            log.GetMessage().ShouldContain("ItemB.A : \"Beta-Alpha\"");
            log.GetMessage().ShouldContain("ItemB : \"Beta\"");
            log.GetMessage().ShouldContain("ItemA : \"Alpha\"");
        }
    }
}