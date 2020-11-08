using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Matchers;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.Matchers
{
    [TestFixture]
    public class NullMatcherTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        [TestCase("Some key value")]
        public void IsMatchAlwaysReturnsFalse(string key)
        {
            NullMatcher.Instance.IsMatch(key).ShouldBeFalse();
        }
    }
}