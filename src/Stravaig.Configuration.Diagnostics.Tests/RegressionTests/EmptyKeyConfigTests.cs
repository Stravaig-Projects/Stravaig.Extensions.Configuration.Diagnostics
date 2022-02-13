using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Stravaig.Configuration.Diagnostics.Logging;
using Stravaig.Extensions.Logging.Diagnostics.Render;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.RegressionTests;

[TestFixture]
public class EmptyKeyConfigTests : LoggerExtensionsTestBase
{
    [Test]
    public void TwoEmptyKeys()
    {
        SetupConfig(c =>
        {
            c.AddInMemoryCollection(new[]
            {
                //new KeyValuePair<string, string>("", "This key is empty"),
                new KeyValuePair<string, string>(":EmptyTopLevel", "The top level key is empty"),
                new KeyValuePair<string, string>("A:Regular:Key", "This is a normal key"),
            });
        });
        SetupLogger();
        
        Logger.LogConfigurationValuesAsInformation(ConfigRoot);

        var logs = GetLogs();
        logs.RenderLogs(Formatter.SimpleBySequence, Sink.Console);

    }
    
}