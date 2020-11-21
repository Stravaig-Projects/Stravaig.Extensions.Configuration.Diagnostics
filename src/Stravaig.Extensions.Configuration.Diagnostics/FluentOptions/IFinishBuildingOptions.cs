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