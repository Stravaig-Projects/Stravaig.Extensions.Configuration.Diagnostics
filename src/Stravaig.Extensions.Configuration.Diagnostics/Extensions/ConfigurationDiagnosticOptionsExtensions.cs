using System;
using Stravaig.Extensions.Configuration.Diagnostics.Matchers;

namespace Stravaig.Extensions.Configuration.Diagnostics
{
    public static class ConfigurationDiagnosticOptionsExtensions
    {
        public static void BuildConfigurationKeyMatcher(this ConfigurationDiagnosticsOptions options, Action<IKeyMatchBuilder> builder)
        {
            var builtMatcher = BuildMatcher(builder);
            options.ConfigurationKeyMatcher = builtMatcher;
        }
        
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