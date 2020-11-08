using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Obfuscators
{
    [TestFixture]
    public class FixedAsteriskObfuscatorTests
    {
        [Test]
        public void DefaultConstructorEmitsFourAsterisksOnObfuscation()
        {
            new FixedAsteriskObfuscator()
                .Obfuscate("hello, world!")
                .ShouldBe("****");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void EmptySecretDoesNotReturnAsterisks(int fixedLength)
        {
            new FixedAsteriskObfuscator(fixedLength)
                .Obfuscate(string.Empty)
                .ShouldBe(string.Empty);
        }
        
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void NullSecretDoesNotReturnAsterisks(int fixedLength)
        {
            new FixedAsteriskObfuscator(fixedLength)
                .Obfuscate(null)
                .ShouldBe(string.Empty);
        }
        
        [TestCase(1, ExpectedResult = "*")]
        [TestCase(2, ExpectedResult = "**")]
        [TestCase(3, ExpectedResult = "***")]
        [TestCase(4, ExpectedResult = "****")]
        [TestCase(5, ExpectedResult = "*****")]
        public string NumberedConstructorReturnsFixedLengthResult(int fixedLength)
        {
            return new FixedAsteriskObfuscator(fixedLength)
                .Obfuscate("hello, world!");
        }
    }
}