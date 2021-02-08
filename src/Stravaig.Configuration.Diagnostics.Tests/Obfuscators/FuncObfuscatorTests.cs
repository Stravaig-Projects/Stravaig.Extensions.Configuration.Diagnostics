using System;
using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Obfuscators
{
    [TestFixture]
    public class FuncObfuscatorTests
    {
        [Test]
        public void Ctor_ThrowsException_WhenFuncIsNull()
        {
            Should.Throw<ArgumentNullException>(() => new FuncObfuscator(null));
        }

        [Test]
        public void FuncIsCalledWhenObfuscatorRuns()
        {
            bool hasBeenCalled = false;
            string secret = "";
            Func<string, string> func = (s) =>
            {
                secret = s;
                hasBeenCalled = true;
                return "world";
            };
            
            var obfuscator = new FuncObfuscator(func);

            var result = obfuscator.Obfuscate("hello");
            
            result.ShouldBe("world");
            hasBeenCalled.ShouldBeTrue();
            secret.ShouldBe("hello");
        }
    }
}