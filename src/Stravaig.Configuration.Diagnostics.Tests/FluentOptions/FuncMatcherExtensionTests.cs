using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics;
using Stravaig.Configuration.Diagnostics.Extensions;
using Stravaig.Configuration.Diagnostics.FluentOptions;
using Stravaig.Configuration.Diagnostics.Matchers;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.FluentOptions
{
    [TestFixture]
    public class FuncMatcherExtensionTests
    {
        [Test]
        public void CheckContainingExtensionMethod()
        {
            var options = SetupByMatchingConfigurationKeys()
                .Matching(k => k == "abc")
                .AndFinally.BuildOptions();

            options.ConfigurationKeyMatcher.ShouldBeOfType<FuncMatcher>();
            options.ConfigurationKeyMatcher.IsMatch("abc").ShouldBeTrue();
            options.ConfigurationKeyMatcher.IsMatch("ABC").ShouldBeFalse();
        }
        
        [Test]
        public void CheckWhereKeyMatchesPredicateExtensionMethods()
        {
            var options = SetupByMatchingConfigurationKeys()
                .Where.KeyMatches(k => k == "abc")
                .AndFinally.BuildOptions();

            options.ConfigurationKeyMatcher.ShouldBeOfType<FuncMatcher>();
            options.ConfigurationKeyMatcher.IsMatch("abc").ShouldBeTrue();
            options.ConfigurationKeyMatcher.IsMatch("ABC").ShouldBeFalse();
        }

        [Test]
        public void CheckWhereKeyMatchesPredicatesExtensionMethods()
        {
            var options = SetupByMatchingConfigurationKeys()
                .Where.KeyMatches(k => k == "abc")
                .OrMatches(k => k == "def")
                .AndFinally.BuildOptions();

            options.ConfigurationKeyMatcher.ShouldBeOfType<AggregateMatcher>();
            options.ConfigurationKeyMatcher.IsMatch("abc").ShouldBeTrue();
            options.ConfigurationKeyMatcher.IsMatch("ABC").ShouldBeFalse();
            options.ConfigurationKeyMatcher.IsMatch("def").ShouldBeTrue();
            options.ConfigurationKeyMatcher.IsMatch("DEF").ShouldBeFalse();
        }

        
        private static IKeyMatcherOptionsBuilder SetupByMatchingConfigurationKeys()
        {
            return ConfigurationDiagnosticsOptions.SetupBy
                .MatchingConfigurationKeys;
        }
    }
}