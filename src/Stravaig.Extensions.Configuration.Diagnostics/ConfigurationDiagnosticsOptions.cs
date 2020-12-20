using System;
using Stravaig.Extensions.Configuration.Diagnostics.FluentOptions;
using Stravaig.Extensions.Configuration.Diagnostics.Matchers;
using Stravaig.Extensions.Configuration.Diagnostics.Obfuscators;
using Stravaig.Extensions.Configuration.Diagnostics.Renderers;

namespace Stravaig.Extensions.Configuration.Diagnostics
{
    /// <summary>
    /// A container for options for the configuration diagnostics.
    /// </summary>
    public class ConfigurationDiagnosticsOptions
    {
        private ISecretObfuscator _obfuscator = PlainTextObfuscator.Instance;
        private IMatcher _configurationKeyMatcher = NullMatcher.Instance;
        private IMatcher _connectionStringElementMatcher = NullMatcher.Instance;
        private IConfigurationKeyRenderer _configurationKeyRenderer = StructuredConfigurationKeyRenderer.Instance;
        private IConnectionStringRenderer _connectionStringRenderer = StructuredConnectionStringRenderer.Instance;
        private IAllConnectionStringsRenderer _allConnectionStringRenderer = StructuredAllConnectionStringsRenderer.Instance;

        /// <summary>
        /// Global options used if no specific options are set.
        /// </summary>
        public static ConfigurationDiagnosticsOptions GlobalOptions { get; } = new ConfigurationDiagnosticsOptions();

        /// <summary>
        /// Initiate the fluent configuration options builder.
        /// </summary>
        public static ConfigurationDiagnosticsOptionsBuilder SetupBy => new ConfigurationDiagnosticsOptionsBuilder();
        
        /// <summary>
        /// The obfuscator to use to mask out secrets.
        /// </summary>
        public ISecretObfuscator Obfuscator
        {
            get => _obfuscator;
            set => _obfuscator = value ?? PlainTextObfuscator.Instance;
        }

        /// <summary>
        /// The matcher that identifies which configuration elements are secrets.
        /// </summary>
        public IMatcher ConfigurationKeyMatcher
        {
            get => _configurationKeyMatcher;
            set => _configurationKeyMatcher = value ?? NullMatcher.Instance;
        }

        /// <summary>
        /// The matcher that identifies which parts of a connection string are secret.
        /// </summary>
        public IMatcher ConnectionStringElementMatcher
        {
            get => _connectionStringElementMatcher;
            set => _connectionStringElementMatcher = value ?? NullMatcher.Instance;
        }
        
        /// <summary>
        /// The renderer that creates the log message for all configuration values in a configuration.
        /// </summary>
        public IConfigurationKeyRenderer ConfigurationKeyRenderer
        {
            get => _configurationKeyRenderer;
            set => _configurationKeyRenderer = value ?? StructuredConfigurationKeyRenderer.Instance;
        }

        /// <summary>
        /// The renderer that creates the log message for a deconstructed connection string.
        /// </summary>
        public IConnectionStringRenderer ConnectionStringRenderer
        {
            get => _connectionStringRenderer;
            set => _connectionStringRenderer = value ?? StructuredConnectionStringRenderer.Instance;
        }

        /// <summary>
        /// The renderer that creates the log message for all deconstructed connection strings in a configuration.
        /// </summary>
        public IAllConnectionStringsRenderer AllConnectionStringsRenderer
        {
            get => _allConnectionStringRenderer;
            set => _allConnectionStringRenderer = value ?? StructuredAllConnectionStringsRenderer.Instance;
        }
        

        /// <summary>
        /// Assists the creation of the <see cref="ConfigurationKeyMatcher"/>.
        /// </summary>
        /// <param name="builder">An <see cref="Action"/> that builds the matcher.</param>
        /// <returns></returns>
        public ConfigurationDiagnosticsOptions BuildConfigurationKeyMatcher(Action<IKeyMatchBuilder> builder)
        {
            KeyMatchBuilder result = new KeyMatchBuilder();
            result.Add(ConfigurationKeyMatcher);
            builder(result);
            ConfigurationKeyMatcher = result.Build();
            return this;
        }

        /// <summary>
        /// Assists the creation of the <see cref="ConnectionStringElementMatcher"/>.
        /// </summary>
        /// <param name="builder">An <see cref="Action"/> that builds the matcher.</param>
        /// <returns></returns>
        public ConfigurationDiagnosticsOptions BuildConnectionStringElementMatcher(Action<IKeyMatchBuilder> builder)
        {
            KeyMatchBuilder result = new KeyMatchBuilder();
            result.Add(ConnectionStringElementMatcher);
            builder(result);
            ConnectionStringElementMatcher = result.Build();
            return this;
        }
    }
}
