using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
        /// Logs the details of all the connection strings in the configuration at the Information level.
        /// </summary>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="config">The configuration to pick up the connection strings.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogAllConnectionStringsAsInformation(this ILogger logger, IConfiguration config,
            ConfigurationDiagnosticsOptions options = null)
        {
            logger.LogAllConnectionStrings(config, LogLevel.Information, options);
        }        

        /// <summary>
        /// Logs the details of all the connection strings in the configuration at the Debug level.
        /// </summary>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="config">The configuration to pick up the connection strings.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogAllConnectionStringsAsDebug(this ILogger logger, IConfiguration config,
            ConfigurationDiagnosticsOptions options = null)
        {
            logger.LogAllConnectionStrings(config, LogLevel.Debug, options);
        }        

        /// <summary>
        /// Logs the details of all the connection strings in the configuration at the Trace level.
        /// </summary>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="config">The configuration to pick up the connection strings.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogAllConnectionStringsAsTrace(this ILogger logger, IConfiguration config,
            ConfigurationDiagnosticsOptions options = null)
        {
            logger.LogAllConnectionStrings(config, LogLevel.Trace, options);
        }        

        /// <summary>
        /// Logs the details of all the connection strings in the configuration.
        /// </summary>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="config">The configuration to pick up the connection strings.</param>
        /// <param name="level">The level to log at.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogAllConnectionStrings(this ILogger logger, IConfiguration config, LogLevel level,
            ConfigurationDiagnosticsOptions options = null)
        {
            var connectionStringSection = config.GetSection("ConnectionStrings");
            var names = connectionStringSection
                .GetChildren()
                .Select(s => s.Key)
                .OrderBy(k => k)
                .ToArray();

            if (names.Length == 0)
            {
                logger.Log(level, "No connections strings found in the configuration.");
                return;
            }

            logger.Log(level, "The following connection strings were found "+string.Join(", ", names));
            
            foreach (string key in names)
            {
                logger.LogConnectionString(config, key, level, options);
            }
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
        /// <param name="connectionStringName">The name of the connection string, or null if there is no name or it is not known.</param>
        /// <param name="options">The options to use, or <see cref="ConfigurationDiagnosticsOptions.GlobalOptions"/> if not specified.</param>
        public static void LogConnectionString(this ILogger logger, LogLevel level, string connectionString, string connectionStringName = null, ConfigurationDiagnosticsOptions options = null)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                logger.Log(level, "No connection string to log.");
                return;
            }

            options = options ?? ConfigurationDiagnosticsOptions.GlobalOptions;

            DbConnectionStringBuilder builder;
            try
            {
                builder = new DbConnectionStringBuilder
                {
                    ConnectionString = connectionString
                };
            }
            catch (Exception ex)
            {
                LogNotAValidConnectionString(logger, level, connectionStringName, ex);
                return;
            }

            List<object> args = new List<object>();
            StringBuilder messageTemplate = new StringBuilder(connectionString.Length);
            BuildStartOfMessage(messageTemplate, connectionStringName, args);
            AddConnectionStringKeysAndValues(messageTemplate, builder, args, options);

            var objArgs = args.ToArray();
            logger.Log(level, messageTemplate.ToString(), objArgs);
        }

        private static void LogNotAValidConnectionString(ILogger logger, LogLevel level, string connectionStringName,
            Exception ex)
        {
            var warningOrGreater = (LogLevel) Math.Max((int) level, (int) LogLevel.Warning);
            StringBuilder sb = new StringBuilder();
            sb.Append("The ");
            if (connectionStringName != null)
                sb.Append($"\"{connectionStringName}\" ");
            sb.Append("connection string value could not be interpreted as a connection string.");
            string message = sb.ToString();
            logger.Log(warningOrGreater, ex, message);
        }

        /// <summary>
        /// Logs the deconstructed connection string at the information level.
        /// </summary>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="connectionString">The connection string to be deconstructed.</param>
        /// <param name="connectionStringName">The name of the connection string, or null if there is no name or it is not known.</param>
        public static void LogConnectionStringAsInformation(this ILogger logger, string connectionString, string connectionStringName = null)
        {
            logger.LogConnectionString(LogLevel.Information, connectionString, connectionStringName);
        }

        /// <summary>
        /// Logs the deconstructed connection string at the debug level.
        /// </summary>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="connectionString">The connection string to be deconstructed.</param>
        /// <param name="connectionStringName">The name of the connection string, or null if there is no name or it is not known.</param>
        public static void LogConnectionStringAsDebug(this ILogger logger, string connectionString, string connectionStringName = null)
        {
            logger.LogConnectionString(LogLevel.Debug, connectionString, connectionStringName);
        }
        
        /// <summary>
        /// Logs the deconstructed connection string at the trace level.
        /// </summary>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="connectionString">The connection string to be deconstructed.</param>
        /// <param name="connectionStringName">The name of the connection string, or null if there is no name or it is not known.</param>
        public static void LogConnectionStringAsTrace(this ILogger logger, string connectionString, string connectionStringName = null)
        {
            logger.LogConnectionString(LogLevel.Trace, connectionString, connectionStringName);
        }

        private static void AddConnectionStringKeysAndValues(
            StringBuilder messageTemplate,
            DbConnectionStringBuilder builder,
            List<object> args,
            ConfigurationDiagnosticsOptions options)
        {
            var keys = builder.Keys ?? Array.Empty<string>();
            int index = 0;
            foreach (var keyObj in keys)
            {
                string key = (string) keyObj;
                messageTemplate.AppendLine(" * {key" + index + "} = {value" + index + "}");
                args.Add(keyObj);
                var value =
                    options.ConnectionStringElementMatcher.IsMatch(key)
                        ? options.Obfuscator.Obfuscate((string) builder[key])
                        : (string) builder[key];
                args.Add(value);
                index++;
            }
        }

        private static void BuildStartOfMessage(
            StringBuilder messageTemplate,
            string connectionStringName,
            List<object> args)
        {
            messageTemplate.Append("Connection string ");
            if (connectionStringName != null)
            {
                messageTemplate.Append("({name}) ");
                args.Add(connectionStringName);
            }

            messageTemplate.AppendLine("parameters:");
        }
    }
}