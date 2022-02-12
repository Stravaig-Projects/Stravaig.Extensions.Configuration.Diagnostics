using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics.Logging;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.RegressionTests
{
    [TestFixture]
    public class BadConnectionStringTests : LoggerExtensionsTestBase
    {
        [Test]
        public void MultipleConnectionStringCannotBeInterpreted()
        {
            SetupConfig(c =>
            {
                c.AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string>("ConnectionStrings:Bad1", "*** I'm the first placeholder, value to be filled in by another provider ***"),
                    new KeyValuePair<string, string>("ConnectionStrings:Good", "server=TheServer;database=TheDatabase;userName=TheUserName;password=ThePassword"),
                    new KeyValuePair<string, string>("ConnectionStrings:Bad2", "*** I'm the second placeholder, value to be filled in by another provider ***"),
                });
            });
            SetupLogger();
            
            Logger.LogAllConnectionStringsAsInformation(ConfigRoot);
            
            var logs = GetLogs();
            var log = logs[0];

            log.Exception.ShouldBeOfType<AggregateException>();
            ((AggregateException)log.Exception).InnerExceptions.Count.ShouldBe(2);
            ((AggregateException)log.Exception).InnerExceptions.ShouldAllBe(e => e.GetType() == typeof(ArgumentException));
            log.OriginalMessage.ShouldContain("The \"{Bad1_name}\" connection string value could not be interpreted as a connection string.");
            log.OriginalMessage.ShouldContain("The \"{Bad2_name}\" connection string value could not be interpreted as a connection string.");
        }
    }
}