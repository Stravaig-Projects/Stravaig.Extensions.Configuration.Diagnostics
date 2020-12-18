using Microsoft.Extensions.Configuration;

namespace Stravaig.Extensions.Configuration.Diagnostics.Renderers
{
    /// <summary>
    /// A Renderer that renders the log message for all connection strings in a configuration.
    /// </summary>
    public interface IAllConnectionStringsRenderer
    {
        /// <summary>
        /// Renders all the connection strings in a configuration.
        /// </summary>
        /// <param name="config">The configuration to extract connection strings from.</param>
        /// <param name="options">The options to use in the rendering.</param>
        /// <returns></returns>
        MessageEntry Render(IConfiguration config, ConfigurationDiagnosticsOptions options);
    }
}