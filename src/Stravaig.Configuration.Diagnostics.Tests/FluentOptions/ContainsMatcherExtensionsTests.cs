using System;
using NUnit.Framework;
using Shouldly;
using Stravaig.Configuration.Diagnostics;
using Stravaig.Configuration.Diagnostics.Extensions;
using Stravaig.Configuration.Diagnostics.FluentOptions;
using Stravaig.Configuration.Diagnostics.Matchers;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.FluentOptions
{
    [TestFixture]
    public class ContainsMatcherExtensionsTests
    {
        [Test]
        public void CheckContainingExtensionMethod()
        {
            var options = SetupByMatchingConfigurationKeys()
                .Containing("abc")
                .AndFinally.BuildOptions();

            options.ConfigurationKeyMatcher.ShouldBeOfType<ContainsMatcher>();
            options.ConfigurationKeyMatcher.IsMatch("abc").ShouldBeTrue();
            options.ConfigurationKeyMatcher.IsMatch("ABC").ShouldBeTrue();
        }
        
        [Test]
        public void CheckContainingComparisonExtensionMethod()
        {
            var options = SetupByMatchingConfigurationKeys()
                .Containing("abc", StringComparison.Ordinal)
                .AndFinally.BuildOptions();

            options.ConfigurationKeyMatcher.ShouldBeOfType<ContainsMatcher>();
            options.ConfigurationKeyMatcher.IsMatch("abc").ShouldBeTrue();
            options.ConfigurationKeyMatcher.IsMatch("ABC").ShouldBeFalse();
        }

        [Test]
        public void CheckWhereKeyContainsExtensionMethods()
        {
            var options = SetupByMatchingConfigurationKeys()
                .Where.KeyContains("abc")
                .AndFinally.BuildOptions();

            options.ConfigurationKeyMatcher.ShouldBeOfType<ContainsMatcher>();
            options.ConfigurationKeyMatcher.IsMatch("abc").ShouldBeTrue();
            options.ConfigurationKeyMatcher.IsMatch("ABC").ShouldBeTrue();
        }

        [Test]
        public void CheckWhereKeyContainsComparisonExtensionMethods()
        {
            var options = SetupByMatchingConfigurationKeys()
                .Where.KeyContains("abc", StringComparison.Ordinal)
                .AndFinally.BuildOptions();

            options.ConfigurationKeyMatcher.ShouldBeOfType<ContainsMatcher>();
            options.ConfigurationKeyMatcher.IsMatch("abc").ShouldBeTrue();
            options.ConfigurationKeyMatcher.IsMatch("ABC").ShouldBeFalse();
        }

        [Test]
        public void CheckWhereKeyContainsOrContainsExtensionMethods()
        {
            var options = SetupByMatchingConfigurationKeys()
                .Where.KeyContains("abc")
                .OrContains("def")
                .AndFinally.BuildOptions();

            options.ConfigurationKeyMatcher.ShouldBeOfType<AggregateMatcher>();
            options.ConfigurationKeyMatcher.IsMatch("abc").ShouldBeTrue();
            options.ConfigurationKeyMatcher.IsMatch("def").ShouldBeTrue();
        }

        [Test]
        public void CheckWhereKeyContainsOrContainsComparisonExtensionMethods()
        {
            var options = SetupByMatchingConfigurationKeys()
                .Where.KeyContains("abc", StringComparison.Ordinal)
                .OrContains("def", StringComparison.Ordinal)
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