using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics;

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
    public void HappyPath()
    {
        var config = ConfigurationDiagnosticsOptions.Setup(opts =>
        {
            opts.MakeGlobal();
            opts.ObfuscateConfig.AddContainsMatcher("password");
            opts.ObfuscateConfig.AddContainsMatcher(new[] {"secret", "token"});
        });

        ConfigurationDiagnosticsOptions.GlobalOptions.ShouldBeSameAs(config);
        config.ConfigurationKeyMatcher.IsMatch("ThisIsMyPassword").ShouldBeTrue();
        config.ConfigurationKeyMatcher.IsMatch("ThisIsMySecret").ShouldBeTrue();
        config.ConfigurationKeyMatcher.IsMatch("ThisIsMyToken").ShouldBeTrue();
        config.ConfigurationKeyMatcher.IsMatch("SomeFeatureFlag").ShouldBeFalse();
    }
}