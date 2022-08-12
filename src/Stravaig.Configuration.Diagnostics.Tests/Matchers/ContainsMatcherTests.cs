using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics.Matchers;

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
        [TestCase("The dog jumps over the fox", "DOG", ExpectedResult = true)]
        public bool IsMatchTests(string candidate, string contains)
        {
            return new ContainsMatcher(contains).IsMatch(candidate);
        }
        
        [TestCase("The dog jumps over the fox", "cat", ExpectedResult = false)]
        [TestCase("The dog jumps over the fox", "dog", ExpectedResult = true)]
        [TestCase("The dog jumps over the fox", "DOG", ExpectedResult = false)]
        public bool IsMatchCaseSensitiveTests(string candidate, string contains)
        {
            return new ContainsMatcher(contains, StringComparison.Ordinal).IsMatch(candidate);
        }

        [TestCaseSource(nameof(IsMatchWithMultipleValuesData))]
        public void IsMatchWithMultipleValues(string candidate, IEnumerable<string> contains, bool expectedResult)
        {
            bool result = new ContainsMatcher(contains).IsMatch(candidate);
            result.ShouldBe(expectedResult);
        }

        public static IEnumerable<object[]> IsMatchWithMultipleValuesData = new[]
        {
            new object[] {"The dog jumps over the fox", new[] {"cat", "bunny"}, false},
            new object[] {"The dog jumps over the fox", new[] {"cat", "dog"}, true},
            new object[] {"The dog jumps over the fox", new[] {"fox", "dog"}, true},
        };
    }
}