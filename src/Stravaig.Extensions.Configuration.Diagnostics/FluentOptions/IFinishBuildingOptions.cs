namespace Stravaig.Extensions.Configuration.Diagnostics.FluentOptions
{
    public interface IFinishBuildingOptions
    {
        ConfigurationDiagnosticsOptions BuildOptions();
        void ApplyOptions(ConfigurationDiagnosticsOptions options);
    }

    public partial class ConfigurationDiagnosticsOptionsBuilder
        : IFinishBuildingOptions
    {
        public ConfigurationDiagnosticsOptions BuildOptions()
        {
            ConfigurationDiagnosticsOptions result = new ConfigurationDiagnosticsOptions();
            ApplyOptions(result);
            return result;
        }

        public void ApplyOptions(ConfigurationDiagnosticsOptions options)
        {
            options.Obfuscator = _obfuscator;
            options.ConfigurationKeyMatcher = _configKeyMatcher;
            options.ConnectionStringElementMatcher = _connectionStringKeyMatcher;
        }
    }
}