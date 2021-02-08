using Microsoft.Extensions.Configuration;

namespace Stravaig.Configuration.Diagnostics.Renderers
{
    /// <summary>
    /// The interface for rendering the configuration source information.
    /// </summary>
    public interface IConfigurationSourceRenderer
    {
        /// <summary>
        /// Renders the configuration source(s) for the given key for use by a logger.
        /// </summary>
        /// <param name="configRoot">The configuration root that contains all the providers.</param>
        /// <param name="key">The key to search for.</param>
        /// <param name="compressed">Whether to skip providers that have no value for this key, or render them against a null value.</param>
        /// <param name="options">The configuration diagnostics options</param>
        /// <returns></returns>
        MessageEntry Render(IConfigurationRoot configRoot, string key, bool compressed,
            ConfigurationDiagnosticsOptions options);
    }
}