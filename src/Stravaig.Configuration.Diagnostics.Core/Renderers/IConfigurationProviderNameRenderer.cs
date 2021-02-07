using Microsoft.Extensions.Configuration;

namespace Stravaig.Extensions.Configuration.Diagnostics.Renderers
{
    /// <summary>
    /// The renderer that renders the log messages for the names of the providers of a configuration.
    /// </summary>
    public interface IConfigurationProviderNameRenderer
    {
        /// <summary>
        /// Renders the names of the configuration providers of the provided configuration.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        /// <returns>A message entry.</returns>
        MessageEntry Render(IConfigurationRoot configuration);
    }
}