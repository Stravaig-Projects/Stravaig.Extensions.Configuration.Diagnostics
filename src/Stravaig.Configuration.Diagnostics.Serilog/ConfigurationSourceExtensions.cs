using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Stravaig.Configuration.Diagnostics.Serilog.Extensions;

namespace Stravaig.Configuration.Diagnostics.Serilog
{
    /// <summary>
    /// Extension methods for tracking where a value came from in the configuration.
    /// </summary>
    [SuppressMessage("ReSharper", "TemplateIsNotCompileTimeConstantProblem")]
    public static class ConfigurationSourceExtensions
    {
        /// <summary>
        /// Logs the source provider(s) for the given configuration key as Trace.
        /// </summary>
        /// <param name="logger">The logger to write to.</param>
        /// <param name="configRoot">The configuration source.</param>
        /// <param name="key">The key to look for.</param>
        /// <param name="compressed">If true, skips logging provider details that have no value for the key.</param>
        /// <param name="options">The options to use when logging, if not set the <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> are used.</param>
        public static void LogConfigurationKeySourceAsVerbose(this ILogger logger, IConfigurationRoot configRoot,
            string key, bool compressed = false, ConfigurationDiagnosticsOptions options = null)
        {
            logger.LogConfigurationKeySource(LogEventLevel.Verbose, configRoot, key, compressed, options);
        }

        /// <summary>
        /// Logs the source provider(s) for the given configuration key as Debug.
        /// </summary>
        /// <param name="logger">The logger to write to.</param>
        /// <param name="configRoot">The configuration source.</param>
        /// <param name="key">The key to look for.</param>
        /// <param name="compressed">If true, skips logging provider details that have no value for the key.</param>
        /// <param name="options">The options to use when logging, if not set the <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> are used.</param>
        public static void LogConfigurationKeySourceAsDebug(this ILogger logger, IConfigurationRoot configRoot,
            string key, bool compressed = false, ConfigurationDiagnosticsOptions options = null)
        {
            logger.LogConfigurationKeySource(LogEventLevel.Debug, configRoot, key, compressed, options);
        }

        /// <summary>
        /// Logs the source provider(s) for the given configuration key as Information.
        /// </summary>
        /// <param name="logger">The logger to write to.</param>
        /// <param name="configRoot">The configuration source.</param>
        /// <param name="key">The key to look for.</param>
        /// <param name="compressed">If true, skips logging provider details that have no value for the key.</param>
        /// <param name="options">The options to use when logging, if not set the <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> are used.</param>
        public static void LogConfigurationKeySourceAsInformation(this ILogger logger, IConfigurationRoot configRoot,
            string key, bool compressed = false, ConfigurationDiagnosticsOptions options = null)
        {
            logger.LogConfigurationKeySource(LogEventLevel.Information, configRoot, key, compressed, options);
        }

        /// <summary>
        /// Logs the source provider(s) for the given configuration key.
        /// </summary>
        /// <param name="logger">The logger to write to.</param>
        /// <param name="level">The log level to write at.</param>
        /// <param name="configRoot">The configuration source.</param>
        /// <param name="key">The key to look for.</param>
        /// <param name="compressed">If true, skips logging provider details that have no value for the key.</param>
        /// <param name="options">The options to use when logging, if not set the <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> are used.</param>
        public static void LogConfigurationKeySource(this ILogger logger, LogEventLevel level, IConfigurationRoot configRoot, string key, bool compressed = false, ConfigurationDiagnosticsOptions options = null)
        {
            if (options == null)
                options = ConfigurationDiagnosticsOptions.GlobalOptions;
            
            var message = options.ConfigurationSourceRenderer.Render(configRoot, key, compressed, options);
            logger.Write(message.GetLogLevel(level), message.Exception, message.MessageTemplate, message.Properties);
        }
    }
}