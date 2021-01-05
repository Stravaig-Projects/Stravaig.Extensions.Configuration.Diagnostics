using Microsoft.Extensions.Configuration;

namespace Stravaig.Extensions.Configuration.Diagnostics.Renderers
{
    /// <summary>
    /// A renderer that renders the log message for all configuration values in a configuration.
    /// </summary>
    public interface IConfigurationKeyRenderer
    {
        /// <summary>
        /// Renders the configuration values in the provided configuration.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="options">The options to use to determine how secrets are rendered.</param>
        /// <returns>A message entry.</returns>
        MessageEntry Render(IConfiguration configuration, ConfigurationDiagnosticsOptions options);
    }
}