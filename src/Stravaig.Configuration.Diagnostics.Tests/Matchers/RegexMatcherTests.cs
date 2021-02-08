using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics.Matchers;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Matchers
{
    [TestFixture]
    public class RegexMatcherTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void RegexCtorWithInvalidInputThrowsException(string causeOfNullArgumentException)
        {
            Should.Throw<ArgumentException>(() => new RegexMatcher(causeOfNullArgumentException));
        }
        
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void RegexWithOptionsCtorWithInvalidInputThrowsException(string causeOfNullArgumentException)
        {
            Should.Throw<ArgumentException>(() => new RegexMatcher(causeOfNullArgumentException, RegexOptions.None));
        }

        [TestCase("Whoever is happy will make others happy too.", "happy", RegexOptions.None, ExpectedResult = true)]
        [TestCase("Whoever is happy will make others happy too.", "sad", RegexOptions.None, ExpectedResult = false)]
        [TestCase("To be or not to be.", "^To be", RegexOptions.None, ExpectedResult = true)]
        [TestCase("To be or not to be.", "^to be", RegexOptions.None, ExpectedResult = false)]
        [TestCase("To be or not to be.", "^TO BE", RegexOptions.IgnoreCase, ExpectedResult = true)]
        public bool IsMatchWithGivenCriteria(string candidate, string pattern, RegexOptions options)
        {
            return new RegexMatcher(pattern, options).IsMatch(candidate);
        }
    }
}