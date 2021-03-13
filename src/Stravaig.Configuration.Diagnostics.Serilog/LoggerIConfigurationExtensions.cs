using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Stravaig.Configuration.Diagnostics.Serilog.Extensions;

namespace Stravaig.Configuration.Diagnostics.Serilog
{
    /// <summary>
    /// Extension method for the IConfiguration interface
    /// </summary>
    // ReSharper disable once InconsistentNaming
    [SuppressMessage("ReSharper", "TemplateIsNotCompileTimeConstantProblem")]
    public static class LoggerIConfigurationExtensions
    {
        /// <summary>
        /// Logs the keys and values in the given configuration object at the information level.
        /// </summary>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="config">The configuration object to examine.</param>
        public static void LogConfigurationValuesAsInformation(this ILogger logger, IConfiguration config)
        {
            logger.LogConfigurationValues(config, LogEventLevel.Information);
        }
        
        /// <summary>
        /// Logs the keys and values in the given configuration object at the information level.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="options">The options to use to obfuscate secrets.</param>
        public static void LogConfigurationValuesAsInformation(this ILogger logger, IConfiguration config, ConfigurationDiagnosticsOptions options)
        {
            logger.LogConfigurationValues(config, LogEventLevel.Information, options);
        }

        /// <summary>
        /// Logs the keys and values in the given configuration object at the debug level.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        public static void LogConfigurationValuesAsDebug(this ILogger logger, IConfiguration config)
        {
            logger.LogConfigurationValues(config, LogEventLevel.Debug);
        }
        
        /// <summary>
        /// Logs the keys and values in the given configuration object at the debug level.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="options">The options to use to obfuscate secrets.</param>
        public static void LogConfigurationValuesAsDebug(this ILogger logger, IConfiguration config, ConfigurationDiagnosticsOptions options)
        {
            logger.LogConfigurationValues(config, LogEventLevel.Debug, options);
        }

        /// <summary>
        /// Logs the keys and values in the given configuration object at the trace level.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        public static void LogConfigurationValuesAsVerbose(this ILogger logger, IConfiguration config)
        {
            logger.LogConfigurationValues(config, LogEventLevel.Verbose);
        }
        
        /// <summary>
        /// Logs the keys and values in the given configuration object at the trace level.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="options">The options to use to obfuscate secrets.</param>
        public static void LogConfigurationValuesAsVerbose(this ILogger logger, IConfiguration config, ConfigurationDiagnosticsOptions options)
        {
            logger.LogConfigurationValues(config, LogEventLevel.Verbose, options);
        }

        /// <summary>
        /// Logs the keys and values in the given configuration object using the <see cref="ConfigurationDiagnosticsOptions"/>'s GlobalOptions.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="level">The level to log at.</param>
        public static void LogConfigurationValues(this ILogger logger, IConfiguration config, LogEventLevel level)
        {
            logger.LogConfigurationValues(config, level, ConfigurationDiagnosticsOptions.GlobalOptions);
        }
        
        /// <summary>
        /// Logs the keys and values in the given configuration object.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="level">The level to log at.</param>
        /// <param name="options">The options to use to obfuscate secrets.</param>
        public static void LogConfigurationValues(this ILogger logger, IConfiguration config, LogEventLevel level, ConfigurationDiagnosticsOptions options)
        {
            var message = options.ConfigurationKeyRenderer.Render(config, options);
            logger.Write(message.GetLogLevel(level), message.Exception, message.MessageTemplate, message.Properties);
        }
    }
}