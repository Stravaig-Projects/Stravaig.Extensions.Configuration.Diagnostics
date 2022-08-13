using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics;
using Stravaig.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Options;

[TestFixture]
public class FixedStringObfuscatorSetupTests
{
    [Test]
    [TestCase("wibble")]
    [TestCase("wobble")]
    public void SetupSpecificStrings(string replacementText)
    {
        var config = ConfigurationDiagnosticsOptions.Setup(opts =>
        {
            opts.SetFixedStringObfuscator(replacementText);
        });
        config.Obfuscator.ShouldBeOfType<FixedStringObfuscator>();
        var obfuscatedValue = config.Obfuscator.Obfuscate("hello, world!");
        obfuscatedValue.ShouldBe(replacementText);
    }
}