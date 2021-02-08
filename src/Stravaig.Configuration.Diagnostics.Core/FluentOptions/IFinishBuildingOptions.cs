namespace Stravaig.Configuration.Diagnostics.FluentOptions
{
    /// <summary>
    /// Interface providing options for completing the fluent build process.
    /// </summary>
    public interface IFinishBuildingOptions
    {
        /// <summary>
        /// Build the options into a new <see cref="ConfigurationDiagnosticsOptions"/> object.
        /// </summary>
        /// <returns>The built options.</returns>
        ConfigurationDiagnosticsOptions BuildOptions();

        /// <summary>
        /// Apply the options to an existing <see cref="ConfigurationDiagnosticsOptions"/> object.
        /// </summary>
        /// <param name="options">The object to apply the options to.</param>
        void ApplyOptions(ConfigurationDiagnosticsOptions options);
    }

    public partial class ConfigurationDiagnosticsOptionsBuilder
        : IFinishBuildingOptions
    {
        ConfigurationDiagnosticsOptions IFinishBuildingOptions.BuildOptions()
        {
            ConfigurationDiagnosticsOptions result = new ConfigurationDiagnosticsOptions();
            ApplyOptionsImpl(result);
            return result;
        }

        void IFinishBuildingOptions.ApplyOptions(ConfigurationDiagnosticsOptions options)
        {
            ApplyOptionsImpl(options);
        }

        private void ApplyOptionsImpl(ConfigurationDiagnosticsOptions options)
        {
            options.Obfuscator = _obfuscator;
            options.ConfigurationKeyMatcher = _configKeyMatcher.Build();
            options.ConnectionStringElementMatcher = _connectionStringKeyMatcher.Build();
        }
    }
}