namespace Stravaig.Extensions.Configuration.Diagnostics.FluentOptions
{
    public partial class ConfigurationDiagnosticsOptionsBuilder
    {
        private enum State
        {
            None,
            BuildingObfuscator,
            BuildingConfigurationKeyMatcher,
            BuildingConnectionStringKeyMatcher,
        }
    }
}