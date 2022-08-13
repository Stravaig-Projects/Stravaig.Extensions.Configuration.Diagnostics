using System.Linq;
using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics;
using Stravaig.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Options;

[TestFixture]
public class MatchedLengthAsteriskObfuscatorSetupTests
{
    [Test]
    [TestCase("short secret", ExpectedResult = 12)]
    [TestCase("a longer secret", ExpectedResult = 15)]
    [TestCase("a really long secret", ExpectedResult = 20)]
    public int SetupFuncObfuscator(string secret)
    {
        var config = ConfigurationDiagnosticsOptions.Setup(opts =>
        {
            opts.SetMatchedLengthObfuscator();
        });
        config.Obfuscator.ShouldBeOfType<MatchedLengthAsteriskObfuscator>();
        var obfuscatedSecret = config.Obfuscator.Obfuscate(secret);
        obfuscatedSecret.All(c => c == '*').ShouldBeTrue();
        return obfuscatedSecret.Length;
    }
}