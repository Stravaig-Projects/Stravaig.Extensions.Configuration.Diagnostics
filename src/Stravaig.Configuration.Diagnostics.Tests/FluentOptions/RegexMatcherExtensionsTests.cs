using System.Text.RegularExpressions;
using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics;
using Stravaig.Configuration.Diagnostics.FluentOptions;
using Stravaig.Configuration.Diagnostics.Matchers;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.FluentOptions
{
    [TestFixture]
    public class RegexMatcherExtensionsTests
    {
        [Test]
        public void CheckMatchingPatternExtensionMethod()
        {
            var options = SetupByMatchingConfigurationKeys()
                .MatchingPattern("abc")
                .AndFinally.BuildOptions();

            options.ConfigurationKeyMatcher.ShouldBeOfType<RegexMatcher>();
            options.ConfigurationKeyMatcher.IsMatch("abc").ShouldBeTrue();
            options.ConfigurationKeyMatcher.IsMatch("ABC").ShouldBeTrue();
        }
        
        [Test]
        public void CheckMatchingPatternWithOptionsExtensionMethod()
        {
            var options = SetupByMatchingConfigurationKeys()
                .MatchingPattern("abc", RegexOptions.None)
                .AndFinally.BuildOptions();

            options.ConfigurationKeyMatcher.ShouldBeOfType<RegexMatcher>();
            options.ConfigurationKeyMatcher.IsMatch("abc").ShouldBeTrue();
            options.ConfigurationKeyMatcher.IsMatch("ABC").ShouldBeFalse();
        }
        
        [Test]
        public void CheckWhereKeyMatchesPatternExtensionMethod()
        {
            var options = SetupByMatchingConfigurationKeys()
                .Where.KeyMatchesPattern("abc")
                .AndFinally.BuildOptions();

            options.ConfigurationKeyMatcher.ShouldBeOfType<RegexMatcher>();
            options.ConfigurationKeyMatcher.IsMatch("abc").ShouldBeTrue();
            options.ConfigurationKeyMatcher.IsMatch("ABC").ShouldBeTrue();
        }

        [Test]
        public void CheckWhereKeyMatchesPatternOrMatchesPatternExtensionMethod()
        {
            var options = SetupByMatchingConfigurationKeys()
                .Where.KeyMatchesPattern("abc")
                .OrMatchesPattern("def")
                .AndFinally.BuildOptions();

            options.ConfigurationKeyMatcher.ShouldBeOfType<AggregateMatcher>();
            options.ConfigurationKeyMatcher.IsMatch("abc").ShouldBeTrue();
            options.ConfigurationKeyMatcher.IsMatch("ABC").ShouldBeTrue();
            options.ConfigurationKeyMatcher.IsMatch("def").ShouldBeTrue();
            options.ConfigurationKeyMatcher.IsMatch("DEF").ShouldBeTrue();
        }

        [Test]
        public void CheckWhereKeyMatchesPatternOrMatchesPatternWithOptionsExtensionMethod()
        {
            var options = SetupByMatchingConfigurationKeys()
                .Where.KeyMatchesPattern("abc", RegexOptions.None)
                .OrMatchesPattern("def", RegexOptions.None)
                .AndFinally.BuildOptions();

            options.ConfigurationKeyMatcher.ShouldBeOfType<AggregateMatcher>();
            options.ConfigurationKeyMatcher.IsMatch("abc").ShouldBeTrue();
            options.ConfigurationKeyMatcher.IsMatch("ABC").ShouldBeFalse();
            options.ConfigurationKeyMatcher.IsMatch("def").ShouldBeTrue();
            options.ConfigurationKeyMatcher.IsMatch("DEF").ShouldBeFalse();
        }

        [Test]
        public void CheckWhereKeyMatchesPatternWithOptionsExtensionMethod()
        {
            var options = SetupByMatchingConfigurationKeys()
                .Where.KeyMatchesPattern("abc", RegexOptions.None)
                .AndFinally.BuildOptions();

            options.ConfigurationKeyMatcher.ShouldBeOfType<RegexMatcher>();
            options.ConfigurationKeyMatcher.IsMatch("abc").ShouldBeTrue();
            options.ConfigurationKeyMatcher.IsMatch("ABC").ShouldBeFalse();
        }

        
        private static IKeyMatcherOptionsBuilder SetupByMatchingConfigurationKeys()
        {
            return ConfigurationDiagnosticsOptions.SetupBy
                .MatchingConfigurationKeys;
        }
    }
}