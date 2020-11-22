using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Matchers;
using Stravaig.Extensions.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.FluentOptions.FluentOptions
{
    [TestFixture]
    public class FluentOptionsTests
    {
        [Test]
        public void BuildObfuscatorOption()
        {
            var options = ConfigurationDiagnosticsOptions.SetupBy
                .Obfuscating.With(new RedactedObfuscator())
                .AndFinally.BuildOptions();

            options.Obfuscator.ShouldNotBeNull();
            options.Obfuscator.ShouldBeOfType<RedactedObfuscator>();
            
            // Should still have default matchers if not set up.
            options.ConfigurationKeyMatcher.ShouldNotBeNull();
            options.ConfigurationKeyMatcher.ShouldBeOfType<NullMatcher>();
            options.ConnectionStringElementMatcher.ShouldNotBeNull();
            options.ConnectionStringElementMatcher.ShouldBeOfType<NullMatcher>();
        }

        [Test]
        public void BuildConfigurationKeyMatcher_MatchingRoute()
        {
            var options = ConfigurationDiagnosticsOptions.SetupBy
                .MatchingConfigurationKeys.Matching(new ContainsMatcher("hello"))
                .AndFinally.BuildOptions();

            options.ConfigurationKeyMatcher.ShouldNotBeNull();
            options.ConfigurationKeyMatcher.ShouldBeOfType<ContainsMatcher>();
            
            // Should still have the default 
            options.Obfuscator.ShouldNotBeNull();
            options.Obfuscator.ShouldBeOfType<PlainTextObfuscator>();
            options.ConnectionStringElementMatcher.ShouldNotBeNull();
            options.ConnectionStringElementMatcher.ShouldBeOfType<NullMatcher>();
        }
        
        [Test]
        public void BuildConfigurationKeyMatcher_WhereSingularRoute()
        {
            var options = ConfigurationDiagnosticsOptions.SetupBy
                .MatchingConfigurationKeys.Where
                .KeyMatches(new ContainsMatcher("hello"))
                .AndFinally.BuildOptions();

            options.ConfigurationKeyMatcher.ShouldNotBeNull();
            options.ConfigurationKeyMatcher.ShouldBeOfType<ContainsMatcher>();
            
            // Should still have the default 
            options.Obfuscator.ShouldNotBeNull();
            options.Obfuscator.ShouldBeOfType<PlainTextObfuscator>();
            options.ConnectionStringElementMatcher.ShouldNotBeNull();
            options.ConnectionStringElementMatcher.ShouldBeOfType<NullMatcher>();
        }

        [Test]
        public void BuildConfigurationKeyMatcher_WhereMultipleRoute()
        {
            var options = ConfigurationDiagnosticsOptions.SetupBy
                .MatchingConfigurationKeys.Where
                .KeyMatches(new ContainsMatcher("hello"))
                .OrMatches(new ContainsMatcher("world"))
                .AndFinally.BuildOptions();

            options.ConfigurationKeyMatcher.ShouldNotBeNull();
            options.ConfigurationKeyMatcher.ShouldBeOfType<AggregateMatcher>();
            
            // Should still have the default 
            options.Obfuscator.ShouldNotBeNull();
            options.Obfuscator.ShouldBeOfType<PlainTextObfuscator>();
            options.ConnectionStringElementMatcher.ShouldNotBeNull();
            options.ConnectionStringElementMatcher.ShouldBeOfType<NullMatcher>();
        }
        
        [Test]
        public void BuildConnectionStringKeyMatcher_MatchingRoute()
        {
            var options = ConfigurationDiagnosticsOptions.SetupBy
                .MatchingConnectionStringKeys.Matching(new ContainsMatcher("hello"))
                .AndFinally.BuildOptions();

            options.ConnectionStringElementMatcher.ShouldNotBeNull();
            options.ConnectionStringElementMatcher.ShouldBeOfType<ContainsMatcher>();
            
            // Should still have the default 
            options.Obfuscator.ShouldNotBeNull();
            options.Obfuscator.ShouldBeOfType<PlainTextObfuscator>();
            options.ConfigurationKeyMatcher.ShouldNotBeNull();
            options.ConfigurationKeyMatcher.ShouldBeOfType<NullMatcher>();
        }
        
        [Test]
        public void BuildConnectionStringMatcher_WhereSingularRoute()
        {
            var options = ConfigurationDiagnosticsOptions.SetupBy
                .MatchingConnectionStringKeys.Where
                .KeyMatches(new ContainsMatcher("hello"))
                .AndFinally.BuildOptions();

            options.ConnectionStringElementMatcher.ShouldNotBeNull();
            options.ConnectionStringElementMatcher.ShouldBeOfType<ContainsMatcher>();
            
            // Should still have the default 
            options.Obfuscator.ShouldNotBeNull();
            options.Obfuscator.ShouldBeOfType<PlainTextObfuscator>();
            options.ConfigurationKeyMatcher.ShouldNotBeNull();
            options.ConfigurationKeyMatcher.ShouldBeOfType<NullMatcher>();
        }

        [Test]
        public void BuildConnectionStringMatcher_WhereMultipleRoute()
        {
            var options = ConfigurationDiagnosticsOptions.SetupBy
                .MatchingConnectionStringKeys.Where
                .KeyMatches(new ContainsMatcher("hello"))
                .OrMatches(new ContainsMatcher("world"))
                .AndFinally.BuildOptions();

            options.ConnectionStringElementMatcher.ShouldNotBeNull();
            options.ConnectionStringElementMatcher.ShouldBeOfType<AggregateMatcher>();
            
            // Should still have the default 
            options.Obfuscator.ShouldNotBeNull();
            options.Obfuscator.ShouldBeOfType<PlainTextObfuscator>();
            options.ConfigurationKeyMatcher.ShouldNotBeNull();
            options.ConfigurationKeyMatcher.ShouldBeOfType<NullMatcher>();
        }
        
        [Test]
        public void BuildFullConfiguration()
        {
            var options = ConfigurationDiagnosticsOptions.SetupBy
                .Obfuscating.With(new RedactedObfuscator())
                .And.MatchingConfigurationKeys.Where
                .KeyMatches(new ContainsMatcher("hello"))
                .OrMatches(new ContainsMatcher("world"))
                .And.MatchingConnectionStringKeys.Matching(new ContainsMatcher("password"))
                .AndFinally.BuildOptions();

            options.Obfuscator.ShouldNotBeNull();
            options.Obfuscator.ShouldBeOfType<RedactedObfuscator>();
            options.ConfigurationKeyMatcher.ShouldNotBeNull();
            options.ConfigurationKeyMatcher.ShouldBeOfType<AggregateMatcher>();
            options.ConnectionStringElementMatcher.ShouldNotBeNull();
            options.ConnectionStringElementMatcher.ShouldBeOfType<ContainsMatcher>();
        }
        
        [Test]
        public void ApplyFullConfiguration()
        {
            var options = new ConfigurationDiagnosticsOptions();
            ConfigurationDiagnosticsOptions.SetupBy
                .Obfuscating.With(new RedactedObfuscator())
                .And.MatchingConfigurationKeys.Where
                .KeyMatches(new ContainsMatcher("hello"))
                .OrMatches(new ContainsMatcher("world"))
                .And.MatchingConnectionStringKeys.Matching(new ContainsMatcher("password"))
                .AndFinally.ApplyOptions(options);

            options.Obfuscator.ShouldNotBeNull();
            options.Obfuscator.ShouldBeOfType<RedactedObfuscator>();
            options.ConfigurationKeyMatcher.ShouldNotBeNull();
            options.ConfigurationKeyMatcher.ShouldBeOfType<AggregateMatcher>();
            options.ConnectionStringElementMatcher.ShouldNotBeNull();
            options.ConnectionStringElementMatcher.ShouldBeOfType<ContainsMatcher>();
        }
    }
}