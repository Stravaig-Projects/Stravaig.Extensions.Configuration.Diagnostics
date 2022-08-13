using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics;
using Stravaig.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Options;

[TestFixture]
public class FuncObfuscatorSetupTests
{
    [Test]
    [TestCase("short secret", ExpectedResult = "--12--")]
    [TestCase("a longer secret", ExpectedResult = "--15--")]
    [TestCase("a really long secret", ExpectedResult = "--20--")]
    public string SetupFuncObfuscator(string secret)
    {
        var config = ConfigurationDiagnosticsOptions.Setup(opts =>
        {
            opts.SetFuncObfuscator(s => $"--{s.Length}--");
        });
        config.Obfuscator.ShouldBeOfType<FuncObfuscator>();
        return config.Obfuscator.Obfuscate(secret);
    }
}