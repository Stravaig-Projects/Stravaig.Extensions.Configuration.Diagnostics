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
    }
}