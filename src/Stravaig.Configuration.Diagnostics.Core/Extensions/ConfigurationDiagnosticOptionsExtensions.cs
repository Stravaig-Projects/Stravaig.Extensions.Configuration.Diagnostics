using System;
using Stravaig.Configuration.Diagnostics.Matchers;

namespace Stravaig.Configuration.Diagnostics
{
    /// <summary>
    /// Extension methods for adding configuration key matchers to the configurations options builder.
    /// </summary>
    public static class ConfigurationDiagnosticOptionsExtensions
    {
        /// <summary>
        /// Add configuration key matchers to the configuration options with a builder action.
        /// </summary>
        /// <param name="options">The Configuration Diagnostics Options.</param>
        /// <param name="builder">A key matcher builder action.</param>
        public static void BuildConfigurationKeyMatcher(this ConfigurationDiagnosticsOptions options, Action<IKeyMatchBuilder> builder)
        {
            var builtMatcher = BuildMatcher(builder);
            options.ConfigurationKeyMatcher = builtMatcher;
        }
        
        /// <summary>
        /// Add connection string key matchers to the configuration options with a builder action.
        /// </summary>
        /// <param name="options">The Configuration Diagnostics Options.</param>
        /// <param name="builder">A key matcher builder action.</param>
        public static void BuildConnectionStringElementMatcher(this ConfigurationDiagnosticsOptions options,
            Action<IKeyMatchBuilder> builder)
        {
            var builtMatcher = BuildMatcher(builder);
            options.ConnectionStringElementMatcher = builtMatcher;
        }

        private static IMatcher BuildMatcher(Action<IKeyMatchBuilder> builder)
        {
            var keyMatchBuilder = new KeyMatchBuilder();
            builder(keyMatchBuilder);
            return keyMatchBuilder.Build();
        }
    }
}