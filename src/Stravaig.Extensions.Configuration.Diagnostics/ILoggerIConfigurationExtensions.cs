using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Stravaig.Extensions.Configuration.Diagnostics
{
    /// <summary>
    /// Extension method for the IConfiguration interface
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class ILoggerIConfigurationExtensions
    {
        /// <summary>
        /// Logs the keys and values in the given configuration object at the information level.
        /// </summary>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="config">The configuration object to examine.</param>
        public static void LogConfigurationValuesAsInformation(this ILogger logger, IConfiguration config)
        {
            logger.LogConfigurationValues(config, LogLevel.Information);
        }
        
        /// <summary>
        /// Logs the keys and values in the given configuration object at the information level.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="options">The options to use to obfuscate secrets.</param>
        public static void LogConfigurationValuesAsInformation(this ILogger logger, IConfiguration config, ConfigurationDiagnosticsOptions options)
        {
            logger.LogConfigurationValues(config, LogLevel.Information, options);
        }

        /// <summary>
        /// Logs the keys and values in the given configuration object at the debug level.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        public static void LogConfigurationValuesAsDebug(this ILogger logger, IConfiguration config)
        {
            logger.LogConfigurationValues(config, LogLevel.Debug);
        }
        
        /// <summary>
        /// Logs the keys and values in the given configuration object at the debug level.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="options">The options to use to obfuscate secrets.</param>
        public static void LogConfigurationValuesAsDebug(this ILogger logger, IConfiguration config, ConfigurationDiagnosticsOptions options)
        {
            logger.LogConfigurationValues(config, LogLevel.Debug, options);
        }

        /// <summary>
        /// Logs the keys and values in the given configuration object at the trace level.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        public static void LogConfigurationValuesAsTrace(this ILogger logger, IConfiguration config)
        {
            logger.LogConfigurationValues(config, LogLevel.Trace);
        }
        
        /// <summary>
        /// Logs the keys and values in the given configuration object at the trace level.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="options">The options to use to obfuscate secrets.</param>
        public static void LogConfigurationValuesAsTrace(this ILogger logger, IConfiguration config, ConfigurationDiagnosticsOptions options)
        {
            logger.LogConfigurationValues(config, LogLevel.Trace, options);
        }

        /// <summary>
        /// Logs the keys and values in the given configuration object using the <see cref="ConfigurationDiagnosticsOptions"/>'s GlobalOptions.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="level">The level to log at.</param>
        public static void LogConfigurationValues(this ILogger logger, IConfiguration config, LogLevel level)
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
        public static void LogConfigurationValues(this ILogger logger, IConfiguration config, LogLevel level, ConfigurationDiagnosticsOptions options)
        {
            
            var valueMap = config.AsEnumerable()
                .OrderBy(kvp => kvp.Key)
                .Select(kvp => Obfuscate(kvp, options))
                .Select(Render);
            string message = "The following values are available:" +
                             Environment.NewLine +
                             string.Join(Environment.NewLine, valueMap);
            
            logger.Log(level, message);
        }

        private static string Render(KeyValuePair<string, string> kvp)
        {
            return $"{kvp.Key} : {kvp.Value}";
        }

        private static KeyValuePair<string, string> Obfuscate(KeyValuePair<string, string> kvp, ConfigurationDiagnosticsOptions options)
        {
            return options.ConfigurationKeyMatcher.IsMatch(kvp.Key)
                ? new KeyValuePair<string, string>(kvp.Key, options.Obfuscator.Obfuscate(kvp.Value))
                : kvp;
        }
    }
}