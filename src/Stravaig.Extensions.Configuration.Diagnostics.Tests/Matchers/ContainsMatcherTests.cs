using System;
using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Matchers;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Matchers
{
    [TestFixture]
    public class ContainsMatcherTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void CtorWithInvalidInputThrowsException(string causeOfNullArgumentException)
        {
            Should.Throw<ArgumentException>(() => new ContainsMatcher(causeOfNullArgumentException));
        }

        [TestCase("The dog jumps over the fox", "cat", ExpectedResult = false)]
        [TestCase("The dog jumps over the fox", "dog", ExpectedResult = true)]
        [TestCase("The dog jumps over the fox", "DOG", ExpectedResult = false)]
        public bool IsMatchTests(string candidate, string contains)
        {
            return new ContainsMatcher(contains).IsMatch(candidate);
        }
    }
}