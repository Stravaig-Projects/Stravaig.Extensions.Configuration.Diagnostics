using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics;
using Stravaig.Configuration.Diagnostics.Matchers;
using Stravaig.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Options;

[TestFixture]
public class SetupOptionsTests
{
    [Test]
    public void ReturnsGlobalConfigOnDemand()
    {
        var config = ConfigurationDiagnosticsOptions.Setup(opts =>
        {
            opts.MakeGlobal();
        });
        
        ConfigurationDiagnosticsOptions.GlobalOptions.ShouldBeSameAs(config);
    }
    
    [Test]
    public void ReturnsLocalConfigByDefault()
    {
        var config = ConfigurationDiagnosticsOptions.Setup(_ => {});
        
        ConfigurationDiagnosticsOptions.GlobalOptions.ShouldNotBeSameAs(config);
    }
    
    [Test]
    public void ReturnsLocalConfigOnDemand()
    {
        var config = ConfigurationDiagnosticsOptions.Setup(opts =>
        {
            opts.MakeGlobal(false);
        });
        
        ConfigurationDiagnosticsOptions.GlobalOptions.ShouldNotBeSameAs(config);
    }

    [Test]
    public void UnsetMatchersAreNullMatchers()
    {
        var config = ConfigurationDiagnosticsOptions.Setup(_ => { });
        config.ConfigurationKeyMatcher.ShouldBeSameAs(NullMatcher.Instance);
        config.ConnectionStringElementMatcher.ShouldBeSameAs(NullMatcher.Instance);
    }
    
    [Test]
    public void UnsetObfuscatorIsPlainTextObfuscator()
    {
        var config = ConfigurationDiagnosticsOptions.Setup(_ => { });
        config.Obfuscator.ShouldBeOfType<PlainTextObfuscator>();
    }

    [Test]
    public void HappyPath()
    {
        var config = ConfigurationDiagnosticsOptions.Setup(opts =>
        {
            opts.MakeGlobal();
            opts.ObfuscateConfig.AddContainsMatcher("password");
            opts.ObfuscateConfig.AddContainsMatcher(new[] {"secret", "token"});

            opts.ObfuscateConnectionString.AddContainsMatcher("password");
        });

        ConfigurationDiagnosticsOptions.GlobalOptions.ShouldBeSameAs(config);
        config.ConfigurationKeyMatcher.IsMatch("ThisIsMyPassword").ShouldBeTrue();
        config.ConfigurationKeyMatcher.IsMatch("ThisIsMySecret").ShouldBeTrue();
        config.ConfigurationKeyMatcher.IsMatch("ThisIsMyToken").ShouldBeTrue();
        config.ConfigurationKeyMatcher.IsMatch("SomeFeatureFlag").ShouldBeFalse();

        config.ConnectionStringElementMatcher.IsMatch("Password").ShouldBeTrue();
        config.ConnectionStringElementMatcher.IsMatch("userId").ShouldBeFalse();
    }
}