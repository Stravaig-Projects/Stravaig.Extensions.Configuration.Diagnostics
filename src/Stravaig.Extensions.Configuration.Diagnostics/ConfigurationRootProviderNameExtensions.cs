using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Stravaig.Extensions.Configuration.Diagnostics
{
    public static class ConfigurationRootProviderNameExtensions
    {
        /// <summary>
        /// Logs the provider names in the given <see cref="T:Microsoft.Extensions.Configuration.IConfigurationRoot"/>
        /// at the Information log level.
        /// </summary>
        /// <param name="config">The configuration root to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        public static void LogProviderNamesAsInformation(this IConfigurationRoot config, ILogger logger)
        {
            config.LogProviderNames(logger, LogLevel.Information);
        }

        /// <summary>
        /// Logs the provider names in the given <see cref="T:Microsoft.Extensions.Configuration.IConfigurationRoot"/>
        /// at the Debug log level.
        /// </summary>
        /// <param name="config">The configuration root to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        public static void LogProviderNamesAsDebug(this IConfigurationRoot config, ILogger logger)
        {
            config.LogProviderNames(logger, LogLevel.Debug);
        }

        /// <summary>
        /// Logs the provider names in the given <see cref="T:Microsoft.Extensions.Configuration.IConfigurationRoot"/>
        /// at the Trace log level.
        /// </summary>
        /// <param name="config">The configuration root to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        public static void LogProviderNamesAsTrace(this IConfigurationRoot config, ILogger logger)
        {
            config.LogProviderNames(logger, LogLevel.Trace);
        }

        /// <summary>
        /// Logs the provider names in the given <see cref="T:Microsoft.Extensions.Configuration.IConfigurationRoot"/>
        /// </summary>
        /// <param name="config">The configuration root to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="level">The log level to use.</param>
        public static void LogProviderNames(this IConfigurationRoot config, ILogger logger, LogLevel level)
        {
            var providerNames = config.Providers
                .Select(p => p.GetType().FullName);
            string message = "The following configuration providers were registered:" +
                             Environment.NewLine +
                             string.Join(Environment.NewLine, providerNames);
            logger.Log(level, message);
        }
    }
}