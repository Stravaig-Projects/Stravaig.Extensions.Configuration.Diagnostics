using System;
using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Matchers;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Matchers
{
    [TestFixture]
    public class FuncMatcherTests
    {
        [Test]
        public void Ctor_ThrowsArgumentNullException_WhenFuncIsNull()
        {
            Should.Throw<ArgumentNullException>(() => new FuncMatcher(null));
        }
        
        [Test]
        public void FuncIsCalledWhenMatcherRuns()
        {
            bool hasBeenCalled = false;
            string secret = "";
            Func<string, bool> func = (s) =>
            {
                secret = s;
                hasBeenCalled = true;
                return true;
            };
            
            var matcher = new FuncMatcher(func);

            var result = matcher.IsMatch("hello");
            
            result.ShouldBeTrue();
            hasBeenCalled.ShouldBeTrue();
            secret.ShouldBe("hello");
        }
    }
}