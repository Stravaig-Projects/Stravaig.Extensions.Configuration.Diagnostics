using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Stravaig.Configuration.Diagnostics.Serilog.Extensions;

namespace Stravaig.Configuration.Diagnostics.Serilog
{
    /// <summary>
    /// Extension methods for deconstructing and logging the components of connection strings.
    /// </summary>
    [SuppressMessage("ReSharper", "TemplateIsNotCompileTimeConstantProblem")]
    public static class ConfigurationConnectionStringExtensions
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
            logger.LogAllConnectionStrings(config, LogEventLevel.Information, options);
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
            logger.LogAllConnectionStrings(config, LogEventLevel.Debug, options);
        }        

        /// <summary>
        /// Logs the details of all the connection strings in the configuration at the Trace level.
        /// </summary>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="config">The configuration to pick up the connection strings.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogAllConnectionStringsAsVerbose(this ILogger logger, IConfiguration config,
            ConfigurationDiagnosticsOptions options = null)
        {
            logger.LogAllConnectionStrings(config, LogEventLevel.Verbose, options);
        }        

        /// <summary>
        /// Logs the details of all the connection strings in the configuration.
        /// </summary>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="config">The configuration to pick up the connection strings.</param>
        /// <param name="level">The level to log at.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogAllConnectionStrings(this ILogger logger, IConfiguration config, LogEventLevel level,
            ConfigurationDiagnosticsOptions options = null)
        {
            options = options ?? ConfigurationDiagnosticsOptions.GlobalOptions;
            var message = options.AllConnectionStringsRenderer.Render(config, options);
            logger.Write(message.GetLogLevel(level), message.Exception, message.MessageTemplate, message.Properties);
        }
    }
}