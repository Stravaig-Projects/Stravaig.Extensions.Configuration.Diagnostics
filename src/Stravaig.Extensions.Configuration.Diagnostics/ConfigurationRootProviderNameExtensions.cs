﻿using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Stravaig.Extensions.Configuration.Diagnostics
{
    /// <summary>
    /// Extensions that log information about the configuration root.
    /// </summary>
    public static class ConfigurationRootProviderNameExtensions
    {
        /// <summary>
        /// Logs the provider names in the given <see cref="T:Microsoft.Extensions.Configuration.IConfigurationRoot"/>
        /// at the Information log level.
        /// </summary>
        /// <param name="config">The configuration root to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        public static void LogProviderNamesAsInformation(this ILogger logger, IConfigurationRoot config)
        {
            logger.LogProviderNames(config, LogLevel.Information);
        }

        /// <summary>
        /// Logs the provider names in the given <see cref="T:Microsoft.Extensions.Configuration.IConfigurationRoot"/>
        /// at the Debug log level.
        /// </summary>
        /// <param name="config">The configuration root to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        public static void LogProviderNamesAsDebug(this ILogger logger, IConfigurationRoot config)
        {
            logger.LogProviderNames(config, LogLevel.Debug);
        }

        /// <summary>
        /// Logs the provider names in the given <see cref="T:Microsoft.Extensions.Configuration.IConfigurationRoot"/>
        /// at the Trace log level.
        /// </summary>
        /// <param name="config">The configuration root to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        public static void LogProviderNamesAsTrace(this ILogger logger, IConfigurationRoot config)
        {
            logger.LogProviderNames(config, LogLevel.Trace);
        }

        /// <summary>
        /// Logs the provider names in the given <see cref="T:Microsoft.Extensions.Configuration.IConfigurationRoot"/>
        /// </summary>
        /// <param name="config">The configuration root to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="level">The log level to use.</param>
        public static void LogProviderNames(this ILogger logger, IConfigurationRoot config, LogLevel level)
        {
            var providerNames = config.Providers
                .Select(p => p.GetType().FullName);
            string message = "The following configuration providers were registered:" +
                             Environment.NewLine +
                             string.Join(Environment.NewLine, providerNames);
            logger.Log(level, message);
        }

        public static void LogProvidersAsInformation(this ILogger logger, IConfigurationRoot config)
        {
            logger.LogProviders(config, LogLevel.Information);
        }
        
        public static void LogProvidersAsDebug(this ILogger logger, IConfigurationRoot config)
        {
            logger.LogProviders(config, LogLevel.Debug);
        }

        public static void LogProvidersAsTrace(this ILogger logger, IConfigurationRoot config)
        {
            logger.LogProviders(config, LogLevel.Trace);
        }

        public static void LogProviders(this ILogger logger, IConfigurationRoot config, LogLevel level)
        {
            var providers = config.Providers
                .Select(p => p.ToString());
            string message = "The following configuration providers were registered:" +
                             Environment.NewLine +
                             string.Join(Environment.NewLine, providers);
            logger.Log(level, message);
        }
    }
}