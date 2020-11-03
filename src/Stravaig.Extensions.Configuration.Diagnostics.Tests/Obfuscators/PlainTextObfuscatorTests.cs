using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Obfuscators
{
    [TestFixture]
    public class PlainTextObfuscatorTests
    {
        [Test]
        public void SecretIsUnchanged()
        {
            PlainTextObfuscator.Instance
                .Obfuscate("Hello, World!")
                .ShouldBe("Hello, World!");
        }
    }
}