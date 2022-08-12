using System.Linq;
using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics;
using Stravaig.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Options;

[TestFixture]
public class FixedAsteriskObfuscatorSetupTests
{
    [Test]
    public void SetupDefault()
    {
        var config = ConfigurationDiagnosticsOptions.Setup(opts =>
        {
            opts.SetFixedAsteriskObfuscator();
        });
        config.Obfuscator.ShouldBeOfType<FixedAsteriskObfuscator>();
        config.Obfuscator.Obfuscate("hello, world!").ShouldBe("****");
    }
    
    [Test]
    [TestCase(1)]
    [TestCase(3)]
    [TestCase(5)]
    [TestCase(7)]
    public void SetupSpecificLength(int length)
    {
        var config = ConfigurationDiagnosticsOptions.Setup(opts =>
        {
            opts.SetFixedAsteriskObfuscator(length);
        });
        config.Obfuscator.ShouldBeOfType<FixedAsteriskObfuscator>();
        var obfuscatedValue = config.Obfuscator.Obfuscate("hello, world!");
        obfuscatedValue.Length.ShouldBe(length);
        obfuscatedValue.All(c => c == '*').ShouldBeTrue();
    }
    
}