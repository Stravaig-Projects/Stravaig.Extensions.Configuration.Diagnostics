using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Obfuscators
{
    [TestFixture]
    public class RedactedObfuscatorTests
    {
        [Test]
        public void SecretIsChangedToRedacted()
        {
            new RedactedObfuscator()
                .Obfuscate("Hello World!")
                .ShouldBe("REDACTED");
        }

        [Test]
        public void SecretIsChangedToRedactedWithSymmetricalAccoutrements()
        {
            new RedactedObfuscator("***")
                .Obfuscate("Hello World!")
                .ShouldBe("***REDACTED***");
        }

        [Test]
        public void SecretIsChangedToRedactedWithAsymmetricalAccoutrements()
        {
            new RedactedObfuscator("<==", "==>")
                .Obfuscate("Hello World!")
                .ShouldBe("<==REDACTED==>");
        }

        [Test]
        public void NullSecretIsObfuscatedToEmptyString()
        {
            new RedactedObfuscator()
                .Obfuscate(null)
                .ShouldBe(string.Empty);
        }

        [Test]
        public void WhiteSpaceSecretIsObfuscatedToEmptyString()
        {
            new RedactedObfuscator()
                .Obfuscate(" \t\r\n")
                .ShouldBe(string.Empty);
        }
    }
}