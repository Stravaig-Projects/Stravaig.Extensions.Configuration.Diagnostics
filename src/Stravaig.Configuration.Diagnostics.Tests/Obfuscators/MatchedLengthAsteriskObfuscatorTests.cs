using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Obfuscators
{
    [TestFixture]
    public class MatchedLengthAsteriskObfuscatorTests
    {
        [TestCase("hello", 5)]
        [TestCase("KFC 11 herbs and spices", 23)]
        public void ObfuscatedSecretIsCorrectLength(string secret, int expectedLength)
        {
            var obfuscatedSecret = new MatchedLengthAsteriskObfuscator().Obfuscate(secret);
            obfuscatedSecret.ShouldNotBeNull();
            obfuscatedSecret.Length.ShouldBe(expectedLength);
            obfuscatedSecret.ShouldAllBe(c => c == '*');
        }

        [Test]
        public void NullSecretIsEmptyString()
        {
            new MatchedLengthAsteriskObfuscator()
                .Obfuscate(null)
                .ShouldBe(string.Empty);
        }
    }
}