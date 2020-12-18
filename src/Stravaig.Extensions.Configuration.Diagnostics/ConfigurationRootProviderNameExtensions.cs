using System;
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
            
            logger.Log(
                level,
                "The following configuration providers were registered: {ProviderNames}",
                Environment.NewLine + string.Join(Environment.NewLine, providerNames));
        }

        /// <summary>
        /// Logs a list of the providers the configuration is using at the Information level.
        /// </summary>
        /// <param name="logger">The logger to write the details to.</param>
        /// <param name="config">The configuration root.</param>
        public static void LogProvidersAsInformation(this ILogger logger, IConfigurationRoot config)
        {
            logger.LogProviders(config, LogLevel.Information);
        }
        
        /// <summary>
        /// Logs a list of the providers the configuration is using at the Debug level.
        /// </summary>
        /// <param name="logger">The logger to write the details to.</param>
        /// <param name="config">The configuration root.</param>
        public static void LogProvidersAsDebug(this ILogger logger, IConfigurationRoot config)
        {
            logger.LogProviders(config, LogLevel.Debug);
        }

        /// <summary>
        /// Logs a list of the providers the configuration is using at the Trace level.
        /// </summary>
        /// <param name="logger">The logger to write the details to.</param>
        /// <param name="config">The configuration root.</param>
        public static void LogProvidersAsTrace(this ILogger logger, IConfigurationRoot config)
        {
            logger.LogProviders(config, LogLevel.Trace);
        }

        /// <summary>
        /// Logs a list of the providers the configuration is using.
        /// </summary>
        /// <param name="logger">The logger to write the details to.</param>
        /// <param name="config">The configuration root.</param>
        /// <param name="level">The level to log at.</param>
        public static void LogProviders(this ILogger logger, IConfigurationRoot config, LogLevel level)
        {
            var providers = config.Providers
                .Select(p => p.ToString());
            
            logger.Log(
                level,
                "The following configuration providers were registered: {Providers}",
                Environment.NewLine + string.Join(Environment.NewLine, providers));
        }
    }
}