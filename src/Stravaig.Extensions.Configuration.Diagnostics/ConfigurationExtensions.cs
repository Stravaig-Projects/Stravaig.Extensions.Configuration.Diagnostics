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
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Logs the keys and values in the given configuration object at the information level.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        public static void LogConfigurationValuesAsInformation(this IConfiguration config, ILogger logger)
        {
            config.LogConfigurationValues(logger, LogLevel.Information);
        }
        
        /// <summary>
        /// Logs the keys and values in the given configuration object at the information level.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="options">The options to use to obfuscate secrets.</param>
        public static void LogConfigurationValuesAsInformation(this IConfiguration config, ILogger logger, ConfigurationDiagnosticsOptions options)
        {
            config.LogConfigurationValues(logger, LogLevel.Information, options);
        }

        /// <summary>
        /// Logs the keys and values in the given configuration object at the debug level.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        public static void LogConfigurationValuesAsDebug(this IConfiguration config, ILogger logger)
        {
            config.LogConfigurationValues(logger, LogLevel.Debug);
        }
        
        /// <summary>
        /// Logs the keys and values in the given configuration object at the debug level.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="options">The options to use to obfuscate secrets.</param>
        public static void LogConfigurationValuesAsDebug(this IConfiguration config, ILogger logger, ConfigurationDiagnosticsOptions options)
        {
            config.LogConfigurationValues(logger, LogLevel.Debug, options);
        }

        /// <summary>
        /// Logs the keys and values in the given configuration object at the trace level.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        public static void LogConfigurationValuesAsTrace(this IConfiguration config, ILogger logger)
        {
            config.LogConfigurationValues(logger, LogLevel.Trace);
        }
        
        /// <summary>
        /// Logs the keys and values in the given configuration object at the trace level.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="options">The options to use to obfuscate secrets.</param>
        public static void LogConfigurationValuesAsTrace(this IConfiguration config, ILogger logger, ConfigurationDiagnosticsOptions options)
        {
            config.LogConfigurationValues(logger, LogLevel.Trace, options);
        }

        /// <summary>
        /// Logs the keys and values in the given configuration object using the <see cref="ConfigurationDiagnosticsOptions"/>'s GlobalOptions.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="level">The level to log at.</param>
        public static void LogConfigurationValues(this IConfiguration config, ILogger logger, LogLevel level)
        {
            config.LogConfigurationValues(logger, level, ConfigurationDiagnosticsOptions.GlobalOptions);
        }
        
        /// <summary>
        /// Logs the keys and values in the given configuration object.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="level">The level to log at.</param>
        /// <param name="options">The options to use to obfuscate secrets.</param>
        public static void LogConfigurationValues(this IConfiguration config, ILogger logger, LogLevel level, ConfigurationDiagnosticsOptions options)
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