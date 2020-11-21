using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.FluentOptions.FluentOptions
{
    [TestFixture]
    public class FluentOptionsRedactingExtensionTests
    {
        [Test]
        public void CheckRedactingExtension()
        {
            var options = SetupByObfuscating().ByRedacting()
                .AndFinally.BuildOptions();
            
            CheckRedactedObfuscator(options)
                .ShouldBe("REDACTED");
        }
        
        [Test]
        public void CheckRedactingWithSymmetricalAccoutrementExtension()
        {
            var options = SetupByObfuscating().ByRedacting("*")
                .AndFinally.BuildOptions();
            
            CheckRedactedObfuscator(options)
                .ShouldBe("*REDACTED*");
        }

        [Test]
        public void CheckRedactingWithAsymmetricalAccoutrementExtension()
        {
            var options = SetupByObfuscating().ByRedacting("[", "]")
                .AndFinally.BuildOptions();
            
            CheckRedactedObfuscator(options)
                .ShouldBe("[REDACTED]");
        }

        private static string CheckRedactedObfuscator(ConfigurationDiagnosticsOptions options)
        {
            options.Obfuscator.ShouldBeOfType<RedactedObfuscator>();
            return ((RedactedObfuscator)options.Obfuscator)
                .Obfuscate("a");
        }

        private static IObfuscatorOptionsBuilder SetupByObfuscating()
        {
            return ConfigurationDiagnosticsOptions.SetupBy
                .Obfuscating;
        }
    }
}