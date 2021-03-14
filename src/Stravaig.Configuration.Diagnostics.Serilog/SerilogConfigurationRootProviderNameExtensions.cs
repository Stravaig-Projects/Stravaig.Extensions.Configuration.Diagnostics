using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace Stravaig.Configuration.Diagnostics.Serilog
{
    /// <summary>
    /// Extensions that log information about the configuration root.
    /// </summary>
    [SuppressMessage("ReSharper", "TemplateIsNotCompileTimeConstantProblem")]
    public static class SerilogConfigurationRootProviderNameExtensions
    {
        /// <summary>
        /// Logs the provider names in the given <see cref="T:Microsoft.Extensions.Configuration.IConfigurationRoot"/>
        /// at the Information log level.
        /// </summary>
        /// <param name="config">The configuration root to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        public static void LogProviderNamesAsInformation(this ILogger logger, IConfigurationRoot config)
        {
            logger.LogProviderNames(config, LogEventLevel.Information);
        }

        /// <summary>
        /// Logs the provider names in the given <see cref="T:Microsoft.Extensions.Configuration.IConfigurationRoot"/>
        /// at the Information log level.
        /// </summary>
        /// <param name="config">The configuration root to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="options">The options to use for rendering the provider names.</param>
        public static void LogProviderNamesAsInformation(this ILogger logger, IConfigurationRoot config, ConfigurationDiagnosticsOptions options)
        {
            logger.LogProviderNames(config, LogEventLevel.Information, options);
        }

        /// <summary>
        /// Logs the provider names in the given <see cref="T:Microsoft.Extensions.Configuration.IConfigurationRoot"/>
        /// at the Debug log level.
        /// </summary>
        /// <param name="config">The configuration root to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        public static void LogProviderNamesAsDebug(this ILogger logger, IConfigurationRoot config)
        {
            logger.LogProviderNames(config, LogEventLevel.Debug);
        }

        /// <summary>
        /// Logs the provider names in the given <see cref="T:Microsoft.Extensions.Configuration.IConfigurationRoot"/>
        /// at the Debug log level.
        /// </summary>
        /// <param name="config">The configuration root to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="options">The options to use for rendering the provider names.</param>
        public static void LogProviderNamesAsDebug(this ILogger logger, IConfigurationRoot config, ConfigurationDiagnosticsOptions options)
        {
            logger.LogProviderNames(config, LogEventLevel.Debug, options);
        }

        /// <summary>
        /// Logs the provider names in the given <see cref="T:Microsoft.Extensions.Configuration.IConfigurationRoot"/>
        /// at the Trace log level.
        /// </summary>
        /// <param name="config">The configuration root to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        public static void LogProviderNamesAsTrace(this ILogger logger, IConfigurationRoot config)
        {
            logger.LogProviderNames(config, LogEventLevel.Verbose);
        }

        /// <summary>
        /// Logs the provider names in the given <see cref="T:Microsoft.Extensions.Configuration.IConfigurationRoot"/>
        /// at the Trace log level.
        /// </summary>
        /// <param name="config">The configuration root to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="options">The options to use for rendering the provider names.</param>
        public static void LogProviderNamesAsTrace(this ILogger logger, IConfigurationRoot config, ConfigurationDiagnosticsOptions options)
        {
            logger.LogProviderNames(config, LogEventLevel.Verbose, options);
        }

        /// <summary>
        /// Logs the provider names in the given <see cref="T:Microsoft.Extensions.Configuration.IConfigurationRoot"/>
        /// </summary>
        /// <param name="config">The configuration root to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="level">The log level to use.</param>
        public static void LogProviderNames(this ILogger logger, IConfigurationRoot config, LogEventLevel level)
        {
            logger.LogProviderNames(config, level, ConfigurationDiagnosticsOptions.GlobalOptions);
        }

        /// <summary>
        /// Logs the provider names in the given <see cref="T:Microsoft.Extensions.Configuration.IConfigurationRoot"/>
        /// </summary>
        /// <param name="config">The configuration root to examine.</param>
        /// <param name="logger">The logger to write the results to.</param>
        /// <param name="level">The log level to use.</param>
        /// <param name="options">The options to use for rendering the provider names.</param>
        public static void LogProviderNames(
            this ILogger logger,
            IConfigurationRoot config,
            LogEventLevel level,
            ConfigurationDiagnosticsOptions options)
        {
            var message = options.ConfigurationProviderNameRenderer.Render(config);
            var normalisedLevel = message.GetLogLevel(level);
            logger.Write(normalisedLevel, message.Exception, message.MessageTemplate, message.Properties);
        }

        /// <summary>
        /// Logs a list of the providers the configuration is using at the Information level.
        /// </summary>
        /// <param name="logger">The logger to write the details to.</param>
        /// <param name="config">The configuration root.</param>
        public static void LogProvidersAsInformation(this ILogger logger, IConfigurationRoot config)
        {
            logger.LogProviders(config, LogEventLevel.Information);
        }

        /// <summary>
        /// Logs a list of the providers the configuration is using at the Information level.
        /// </summary>
        /// <param name="logger">The logger to write the details to.</param>
        /// <param name="config">The configuration root.</param>
        /// <param name="options">The options to use for rendering the providers.</param>
        public static void LogProvidersAsInformation(this ILogger logger, IConfigurationRoot config, ConfigurationDiagnosticsOptions options)
        {
            logger.LogProviders(config, LogEventLevel.Information, options);
        }
        
        /// <summary>
        /// Logs a list of the providers the configuration is using at the Debug level.
        /// </summary>
        /// <param name="logger">The logger to write the details to.</param>
        /// <param name="config">The configuration root.</param>
        public static void LogProvidersAsDebug(this ILogger logger, IConfigurationRoot config)
        {
            logger.LogProviders(config, LogEventLevel.Debug);
        }

        /// <summary>
        /// Logs a list of the providers the configuration is using at the Debug level.
        /// </summary>
        /// <param name="logger">The logger to write the details to.</param>
        /// <param name="config">The configuration root.</param>
        /// <param name="options">The options to use for rendering the providers.</param>
        public static void LogProvidersAsDebug(this ILogger logger, IConfigurationRoot config, ConfigurationDiagnosticsOptions options)
        {
            logger.LogProviders(config, LogEventLevel.Debug, options);
        }

        /// <summary>
        /// Logs a list of the providers the configuration is using at the Trace level.
        /// </summary>
        /// <param name="logger">The logger to write the details to.</param>
        /// <param name="config">The configuration root.</param>
        public static void LogProvidersAsTrace(this ILogger logger, IConfigurationRoot config)
        {
            logger.LogProviders(config, LogEventLevel.Verbose);
        }

        /// <summary>
        /// Logs a list of the providers the configuration is using at the Trace level.
        /// </summary>
        /// <param name="logger">The logger to write the details to.</param>
        /// <param name="config">The configuration root.</param>
        /// <param name="options">The options to use for rendering the providers.</param>
        public static void LogProvidersAsTrace(this ILogger logger, IConfigurationRoot config, ConfigurationDiagnosticsOptions options)
        {
            logger.LogProviders(config, LogEventLevel.Verbose, options);
        }

        /// <summary>
        /// Logs a list of the providers the configuration is using.
        /// </summary>
        /// <param name="logger">The logger to write the details to.</param>
        /// <param name="config">The configuration root.</param>
        /// <param name="level">The level to log at.</param>
        public static void LogProviders(this ILogger logger, IConfigurationRoot config, LogEventLevel level)
        {
            logger.LogProviders(config, level, ConfigurationDiagnosticsOptions.GlobalOptions);
        }

        /// <summary>
        /// Logs a list of the providers the configuration is using.
        /// </summary>
        /// <param name="logger">The logger to write the details to.</param>
        /// <param name="config">The configuration root.</param>
        /// <param name="level">The level to log at.</param>
        /// <param name="options">The options to use for rendering the providers.</param>
        public static void LogProviders(
            this ILogger logger,
            IConfigurationRoot config,
            LogEventLevel level,
            ConfigurationDiagnosticsOptions options)
        {
            var message = options.ConfigurationProviderRenderer.Render(config);
            logger.Write(message.GetLogLevel(level), message.Exception, message.MessageTemplate, message.Properties);
        }
    }
}