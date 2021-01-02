using Microsoft.Extensions.Configuration;

namespace Stravaig.Extensions.Configuration.Diagnostics.Renderers
{
    /// <summary>
    /// The renderer that renders the log messages for the providers of a configuration.
    /// </summary>
    public interface IConfigurationProviderRenderer
    {
        /// <summary>
        /// Renders the configuration providers of the provided configuration.
        /// </summary>
        /// <param name="configuration">The application configuration.</param>
        /// <returns>A message entry.</returns>
        MessageEntry Render(IConfigurationRoot configuration);
    }
}