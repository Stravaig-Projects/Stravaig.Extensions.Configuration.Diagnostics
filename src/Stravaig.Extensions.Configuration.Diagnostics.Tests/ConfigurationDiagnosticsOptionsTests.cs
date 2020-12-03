using System;
using NUnit.Framework;
using Shouldly;
using Stravaig.Extensions.Configuration.Diagnostics.Matchers;
using Stravaig.Extensions.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests
{
    [TestFixture]
    public class ConfigurationDiagnosticsOptionsTests
    {
        [Test]
        public void CtorSetsDefaultsOnAllOptions()
        {
            var options = new ConfigurationDiagnosticsOptions();
            options.Obfuscator.ShouldNotBeNull();
            options.Obfuscator.ShouldBeOfType<PlainTextObfuscator>();
            
            options.ConfigurationKeyMatcher.ShouldNotBeNull();
            options.ConfigurationKeyMatcher.ShouldBeOfType<NullMatcher>();

            options.ConnectionStringElementMatcher.ShouldNotBeNull();
            options.ConnectionStringElementMatcher.ShouldBeOfType<NullMatcher>();
        }

        [Test]
        public void BuildConfigurationKeyMatcherAssignsToConfigurationKeyMatcher()
        {
            var options = new ConfigurationDiagnosticsOptions();
            options.BuildConfigurationKeyMatcher(builder =>
            {
                builder.Add(new ContainsMatcher("abc"));
            });

            options.ConfigurationKeyMatcher.ShouldNotBeNull();
            options.ConfigurationKeyMatcher.ShouldNotBeOfType<NullMatcher>();
        }

        [Test]
        public void BuildConnectionStringElementMatcherAssignsToConnectionStringElementMatcher()
        {
            var options = new ConfigurationDiagnosticsOptions();
            options.BuildConnectionStringElementMatcher(builder =>
            {
                builder.Add(new ContainsMatcher("def"));
            });

            options.ConnectionStringElementMatcher.ShouldNotBeNull();
            options.ConnectionStringElementMatcher.ShouldNotBeOfType<NullMatcher>();
        }

        [Test]
        public void ObfuscatorSetterAssignsObfuscator()
        {
            var options = new ConfigurationDiagnosticsOptions();
            options.Obfuscator = new FixedAsteriskObfuscator();

            options.Obfuscator.ShouldNotBeNull();
            options.Obfuscator.ShouldBeOfType<FixedAsteriskObfuscator>();
        }

        [Test]
        public void ObfuscatorSetReplacesNullWithPlainTextObfuscator()
        {
            var options = new ConfigurationDiagnosticsOptions();
            options.Obfuscator = null;

            options.Obfuscator.ShouldNotBeNull();
            options.Obfuscator.ShouldBeOfType<PlainTextObfuscator>();
        }

        [Test]
        public void ConfigurationKeyMatcherSetReplacesNullWithNullMatcher()
        {
            var options = new ConfigurationDiagnosticsOptions();
            options.ConfigurationKeyMatcher = null;
            
            options.ConfigurationKeyMatcher.ShouldNotBeNull();
            options.ConfigurationKeyMatcher.ShouldBeOfType<NullMatcher>();
        }

        [Test]
        public void ConnectionStringElementMatcherSetReplacesNullWithNullMatcher()
        {
            var options = new ConfigurationDiagnosticsOptions();
            options.ConnectionStringElementMatcher = null;
            
            options.ConnectionStringElementMatcher.ShouldNotBeNull();
            options.ConnectionStringElementMatcher.ShouldBeOfType<NullMatcher>();
        }
    }
}