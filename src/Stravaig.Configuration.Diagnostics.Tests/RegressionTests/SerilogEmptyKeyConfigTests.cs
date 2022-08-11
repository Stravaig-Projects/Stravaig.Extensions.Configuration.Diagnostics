using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics.Serilog;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.RegressionTests;

[TestFixture]
public class SerilogEmptyKeyConfigTests : SerilogTestBase
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
        SetupLogger(true);
        Logger.LogConfigurationValuesAsInformation(ConfigRoot);

        Console.WriteLine("---JSON log output---");
        var jsonLogs = GetLogs();
        foreach (var logEntry in jsonLogs)
            Console.WriteLine(logEntry);

        jsonLogs.Count.ShouldBe(1);
        var log = jsonLogs[0];
        var properties = log["Properties"];
        properties.Value<string>("_EmptyTopLevel").ShouldBe("The top level key is empty");
        properties.Value<string>("A_Regular_Key").ShouldBe("This is a normal key");

        var nullProperties = properties
            .Select(p => p.Path.Split('.').Last())
            .Where(p => p is not ("_EmptyTopLevel" or "A_Regular_Key"))
            .ToArray();
        foreach (var nullProperty in nullProperties)
            properties.Value<string>(nullProperty).ShouldBeNull($"Property \"{nullProperty}\" should have a null value.");
        // var nullProperties = properties.Children()
        //     .Where(p => p.Key is not ("_EmptyTopLevel" or "A_Regular_Key" or "{OriginalFormat}"))
        //     .ToArray();
        // foreach(var nullProperty in nullProperties)
        //     nullProperty.Value.ShouldBe("(null)", $"Property \"{nullProperty.Key}\" should have a dummy null value.");
    }
}