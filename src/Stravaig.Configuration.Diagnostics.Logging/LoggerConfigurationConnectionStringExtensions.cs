using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Stravaig.Configuration.Diagnostics.Logging
{
    /// <summary>
    /// Extension methods for deconstructing and logging the components of connection strings.
    /// </summary>
    [SuppressMessage("ReSharper", "TemplateIsNotCompileTimeConstantProblem")]
    public static class LoggerConfigurationConnectionStringExtensions
    {
        /// <summary>
        /// Logs the details of all the connection strings in the configuration at the Information level.
        /// </summary>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="config">The configuration to pick up the connection strings.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogAllConnectionStringsAsInformation(this ILogger logger, IConfiguration config,
            ConfigurationDiagnosticsOptions options = null)
        {
            logger.LogAllConnectionStrings(config, LogLevel.Information, options);
        }        

        /// <summary>
        /// Logs the details of all the connection strings in the configuration at the Debug level.
        /// </summary>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="config">The configuration to pick up the connection strings.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogAllConnectionStringsAsDebug(this ILogger logger, IConfiguration config,
            ConfigurationDiagnosticsOptions options = null)
        {
            logger.LogAllConnectionStrings(config, LogLevel.Debug, options);
        }        

        /// <summary>
        /// Logs the details of all the connection strings in the configuration at the Trace level.
        /// </summary>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="config">The configuration to pick up the connection strings.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogAllConnectionStringsAsTrace(this ILogger logger, IConfiguration config,
            ConfigurationDiagnosticsOptions options = null)
        {
            logger.LogAllConnectionStrings(config, LogLevel.Trace, options);
        }        

        /// <summary>
        /// Logs the details of all the connection strings in the configuration.
        /// </summary>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="config">The configuration to pick up the connection strings.</param>
        /// <param name="level">The level to log at.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogAllConnectionStrings(this ILogger logger, IConfiguration config, LogLevel level,
            ConfigurationDiagnosticsOptions options = null)
        {
            options = options ?? ConfigurationDiagnosticsOptions.GlobalOptions;
            var message = options.AllConnectionStringsRenderer.Render(config, options);
            logger.Log(message.GetLogLevel(level), message.Exception, message.MessageTemplate, message.Properties);
        }
    }
}