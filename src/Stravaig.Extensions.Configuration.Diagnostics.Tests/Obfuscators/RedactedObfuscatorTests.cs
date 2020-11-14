using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Obfuscators;

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
    }
}