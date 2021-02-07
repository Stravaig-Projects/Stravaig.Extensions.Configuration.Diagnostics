namespace Stravaig.Extensions.Configuration.Diagnostics.Renderers
{
    /// <summary>
    /// The renderer that renders the log messages for a deconstructed connection string.
    /// </summary>
    public interface IConnectionStringRenderer
    {
        /// <summary>
        /// Renders a deconstructed connection string.
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <param name="name">The name of the connection string, if available. Can be null.</param>
        /// <param name="options">The options to use to determine how secrets are rendered.</param>
        /// <returns>A message entry.</returns>
        MessageEntry Render(string connectionString, string name, ConfigurationDiagnosticsOptions options);
    }
}