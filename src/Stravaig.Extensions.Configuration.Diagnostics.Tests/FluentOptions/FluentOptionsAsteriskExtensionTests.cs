using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.FluentOptions.FluentOptions
{
    [TestFixture]
    public class FluentOptionsAsteriskExtensionTests
    {
        [Test]
        public void CheckAsteriskExtension()
        {
            var options = SetupByObfuscating().WithAsterisks()
                .AndFinally.BuildOptions();
            
            options.Obfuscator.ShouldBeOfType<MatchedLengthAsteriskObfuscator>();
            var obfuscator = (MatchedLengthAsteriskObfuscator)options.Obfuscator;
            
            obfuscator.Obfuscate("a").ShouldBe("*");
            obfuscator.Obfuscate("abc").ShouldBe("***");
        }
        
        [Test]
        public void CheckFixedAsteriskExtension()
        {
            var options = SetupByObfuscating().WithFixedAsterisks()
                .AndFinally.BuildOptions();
            
            options.Obfuscator.ShouldBeOfType<FixedAsteriskObfuscator>();
            var obfuscator = (FixedAsteriskObfuscator)options.Obfuscator;
            
            obfuscator.Obfuscate("a").ShouldBe("****");
            obfuscator.Obfuscate("abc").ShouldBe("****");
        }
        
        [Test]
        public void CheckFixedTwoAsteriskExtension()
        {
            var options = SetupByObfuscating().WithFixedAsterisks(2)
                .AndFinally.BuildOptions();
            
            options.Obfuscator.ShouldBeOfType<FixedAsteriskObfuscator>();
            var obfuscator = (FixedAsteriskObfuscator)options.Obfuscator;
            
            obfuscator.Obfuscate("a").ShouldBe("**");
            obfuscator.Obfuscate("abc").ShouldBe("**");
        }
        
        private static IObfuscatorOptionsBuilder SetupByObfuscating()
        {
            return ConfigurationDiagnosticsOptions.SetupBy
                .Obfuscating;
        }
    }
}