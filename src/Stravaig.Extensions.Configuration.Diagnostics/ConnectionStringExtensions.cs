using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stravaig.Extensions.Configuration.Diagnostics.Renderers;

namespace Stravaig.Extensions.Configuration.Diagnostics
{
    /// <summary>
    /// Extension methods for deconstructing and logging the components of connection strings.
    /// </summary>
    public static class ConnectionStringExtensions
    {
        /// <summary>
        /// Logs the details of the named connection string at the desired level.
        /// </summary>
        /// <param name="config">The configuration that contains the connection strings.</param>
        /// <param name="name">The name of the connection string to log.</param>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="level">The level to log at.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogConnectionString(this ILogger logger, IConfiguration config, string name, LogLevel level, ConfigurationDiagnosticsOptions options = null)
        {
            string connectionString = config.GetConnectionString(name);
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                logger.Log(
                    level,
                    "There is no connection string in the configuration with the name {name}",
                    name);
            }
            else
            {
                logger.LogConnectionString(level, connectionString, name, options);
            }
        }

        /// <summary>
        /// Logs the details of the named connection string at the information level.
        /// </summary>
        /// <param name="config">The configuration that contains the connection strings.</param>
        /// <param name="name">The name of the connection string to log.</param>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogConnectionStringAsInformation(this ILogger logger, IConfiguration config, string name, ConfigurationDiagnosticsOptions options = null)
        {
            logger.LogConnectionString(config, name, LogLevel.Information, options);
        }

        /// <summary>
        /// Logs the details of the named connection string at the debug level.
        /// </summary>
        /// <param name="config">The configuration that contains the connection strings.</param>
        /// <param name="name">The name of the connection string to log.</param>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogConnectionStringAsDebug(this ILogger logger, IConfiguration config, string name, ConfigurationDiagnosticsOptions options = null)
        {
            logger.LogConnectionString(config, name, LogLevel.Debug, options);
        }

        /// <summary>
        /// Logs the details of the named connection string at the trace level.
        /// </summary>
        /// <param name="config">The configuration that contains the connection strings.</param>
        /// <param name="name">The name of the connection string to log.</param>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogConnectionStringAsTrace(this ILogger logger, IConfiguration config, string name, ConfigurationDiagnosticsOptions options)
        {
            logger.LogConnectionString(config, name, LogLevel.Trace, options);
        }

        /// <summary>
        /// Logs the details of the connection's connection string at the given level.
        /// </summary>
        /// <param name="connection">The connection object.</param>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="level">The level to log at.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogConnectionString(this ILogger logger, IDbConnection connection, LogLevel level, ConfigurationDiagnosticsOptions options = null)
        {
            var connectionString = connection.ConnectionString;
            logger.LogConnectionString(level, connectionString, null, options);
        }

        /// <summary>
        /// Logs the details of the connection's connection string at the information level.
        /// </summary>
        /// <param name="connection">The connection object.</param>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogConnectionStringAsInformation(this ILogger logger, IDbConnection connection, ConfigurationDiagnosticsOptions options = null)
        {
            logger.LogConnectionString(connection, LogLevel.Information, options);
        }

        /// <summary>
        /// Logs the details of the connection's connection string at the debug level.
        /// </summary>
        /// <param name="connection">The connection object.</param>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogConnectionStringAsDebug(this ILogger logger, IDbConnection connection, ConfigurationDiagnosticsOptions options = null)
        {
            logger.LogConnectionString(connection, LogLevel.Debug, options);
        }

        /// <summary>
        /// Logs the details of the connection's connection string at the trace level.
        /// </summary>
        /// <param name="connection">The connection object.</param>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogConnectionStringAsTrace(this ILogger logger, IDbConnection connection, ConfigurationDiagnosticsOptions options = null)
        {
            logger.LogConnectionString(connection, LogLevel.Trace, options);
        }

        /// <summary>
        /// Logs the deconstructed connection string at the desired level.
        /// </summary>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="level">The level to log at.</param>
        /// <param name="connectionString">The connection string to be deconstructed.</param>
        /// <param name="name">The name of the connection string, or null if there is no name or it is not known.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogConnectionString(this ILogger logger, LogLevel level, string connectionString, string name = null, ConfigurationDiagnosticsOptions options = null)
        {
            options = options ?? ConfigurationDiagnosticsOptions.GlobalOptions;
            MessageEntry message = options.ConnectionStringRenderer.Render(connectionString, name, options);
            logger.Log(message.GetLogLevel(level), message.Exception, message.MessageTemplate, message.Properties);
        }

        /// <summary>
        /// Logs the deconstructed connection string at the information level.
        /// </summary>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="connectionString">The connection string to be deconstructed.</param>
        /// <param name="name">The name of the connection string, or null if there is no name or it is not known.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogConnectionStringAsInformation(this ILogger logger, string connectionString, string name = null, ConfigurationDiagnosticsOptions options = null)
        {
            logger.LogConnectionString(LogLevel.Information, connectionString, name, options);
        }

        /// <summary>
        /// Logs the deconstructed connection string at the debug level.
        /// </summary>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="connectionString">The connection string to be deconstructed.</param>
        /// <param name="name">The name of the connection string, or null if there is no name or it is not known.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogConnectionStringAsDebug(this ILogger logger, string connectionString, string name = null, ConfigurationDiagnosticsOptions options = null)
        {
            logger.LogConnectionString(LogLevel.Debug, connectionString, name, options);
        }
        
        /// <summary>
        /// Logs the deconstructed connection string at the trace level.
        /// </summary>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="connectionString">The connection string to be deconstructed.</param>
        /// <param name="name">The name of the connection string, or null if there is no name or it is not known.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogConnectionStringAsTrace(this ILogger logger, string connectionString, string name = null, ConfigurationDiagnosticsOptions options = null)
        {
            logger.LogConnectionString(LogLevel.Trace, connectionString, name, options);
        }
    }
}