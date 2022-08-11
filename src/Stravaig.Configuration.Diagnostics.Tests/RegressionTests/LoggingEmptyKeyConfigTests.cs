using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics.Logging;
using Stravaig.Extensions.Logging.Diagnostics.Render;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.RegressionTests;

[TestFixture]
public class LoggingEmptyKeyConfigTests : LoggerExtensionsTestBase
{
    [Test]
    public void TwoEmptyKeys()
    {
        SetupConfig(c =>
        {
            c.AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string>(":EmptyTopLevel", "The top level key is empty"),
                new KeyValuePair<string, string>("A:Regular:Key", "This is a normal key"),
            });
        });
        
        Console.WriteLine("---Enumerate over the config values---");
        var configValues = ConfigRoot.AsEnumerable()
            .OrderBy(e => e.Key)
            .ToArray();
        foreach (var configElement in configValues)
            Console.WriteLine($"[{configElement.Key}] = \"{configElement.Value}\"");
        
        Console.WriteLine("---Log output---");
        SetupLogger();
        Logger.LogConfigurationValuesAsInformation(ConfigRoot);

        Console.WriteLine("---Captured log output---");
        var logs = GetLogs();
        logs.RenderLogs(Formatter.SimpleBySequence, Console.WriteLine);

        logs.Count.ShouldBe(1);
        var log = logs[0];
        var properties = log.PropertyDictionary;
        properties["_EmptyTopLevel"].ShouldBe("The top level key is empty");
        properties["A_Regular_Key"].ShouldBe("This is a normal key");
        var nullProperties = properties
            .Where(p => p.Key is not ("_EmptyTopLevel" or "A_Regular_Key" or "{OriginalFormat}"))
            .ToArray();
        
#if NET6_0_OR_GREATER
        foreach(var nullProperty in nullProperties)
            nullProperty.Value.ShouldBeNull($"Property \"{nullProperty.Key}\" should have a null value.");
#else
        foreach(var nullProperty in nullProperties)
            nullProperty.Value.ShouldBe("(null)", $"Property \"{nullProperty.Key}\" should have a dummy null value.");
#endif
    }
}