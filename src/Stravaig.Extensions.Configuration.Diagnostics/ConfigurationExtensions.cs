using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Stravaig.Extensions.Configuration.Diagnostics
{
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
        /// Logs the keys and values in the given configuration object at the debug level.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        public static void LogConfigurationValuesAsDebug(this IConfiguration config, ILogger logger)
        {
            config.LogConfigurationValues(logger, LogLevel.Debug);
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
        /// Logs the keys and values in the given configuration object.
        /// </summary>
        /// <param name="config">The configuration object to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="level">The level to log at.</param>
        public static void LogConfigurationValues(this IConfiguration config, ILogger logger, LogLevel level)
        {
            var valueMap = config.AsEnumerable()
                .OrderBy(kvp => kvp.Key)
                .Select(kvp => $"{kvp.Key} : {kvp.Value}");
            string message = "The following values are available:" +
                             Environment.NewLine +
                             string.Join(Environment.NewLine, valueMap);
            
            logger.Log(level, message);
        }
    }
}