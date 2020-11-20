using System;
using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.FluentOptions.FluentOptions
{
    [TestFixture]
    public class FluentOptionsTests
    {
        [Test]
        public void BuildObfuscatorOption()
        {
            var options = ConfigurationDiagnosticsOptions.SetupBy
                .Obfuscating.With(new RedactedObfuscator())
                .AndFinally.BuildOptions();

            options.Obfuscator.ShouldNotBeNull();
            options.Obfuscator.ShouldBeOfType<RedactedObfuscator>();
            
            // Should still have default matchers if not set up.
            options.ConfigurationKeyMatcher.ShouldNotBeNull();
            options.ConnectionStringElementMatcher.ShouldNotBeNull();
        }
    }
}