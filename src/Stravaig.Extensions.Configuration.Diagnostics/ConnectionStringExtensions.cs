using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Stravaig.Extensions.Configuration.Diagnostics
{
    public static class ConnectionStringExtensions
    {
        /// <summary>
        /// Logs the details of the named connection string at the desired level.
        /// </summary>
        /// <param name="config">The configuration that contains the connection strings.</param>
        /// <param name="name">The name of the connection string to log.</param>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="level">The level to log at.</param>
        public static void LogConnectionString(this IConfiguration config, string name, ILogger logger, LogLevel level)
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
                logger.LogConnectionString(level, connectionString, name);
            }
        }

        /// <summary>
        /// Logs the details of the named connection string at the information level.
        /// </summary>
        /// <param name="config">The configuration that contains the connection strings.</param>
        /// <param name="name">The name of the connection string to log.</param>
        /// <param name="logger">The logger to send the details to.</param>
        public static void LogConnectionStringAsInformation(this IConfiguration config, string name, ILogger logger)
        {
            config.LogConnectionString(name, logger, LogLevel.Information);
        }

        /// <summary>
        /// Logs the details of the named connection string at the debug level.
        /// </summary>
        /// <param name="config">The configuration that contains the connection strings.</param>
        /// <param name="name">The name of the connection string to log.</param>
        /// <param name="logger">The logger to send the details to.</param>
        public static void LogConnectionStringAsDebug(this IConfiguration config, string name, ILogger logger)
        {
            config.LogConnectionString(name, logger, LogLevel.Debug);
        }

        /// <summary>
        /// Logs the details of the named connection string at the trace level.
        /// </summary>
        /// <param name="config">The configuration that contains the connection strings.</param>
        /// <param name="name">The name of the connection string to log.</param>
        /// <param name="logger">The logger to send the details to.</param>
        public static void LogConnectionStringAsTrace(this IConfiguration config, string name, ILogger logger)
        {
            config.LogConnectionString(name, logger, LogLevel.Trace);
        }

        /// <summary>
        /// Logs the details of the connection's connection string at the given level.
        /// </summary>
        /// <param name="connection">The connection object.</param>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="level">The level to log at.</param>
        public static void LogConnectionString(this IDbConnection connection, ILogger logger, LogLevel level)
        {
            var connectionString = connection.ConnectionString;
            logger.LogConnectionString(level, connectionString);
        }

        /// <summary>
        /// Logs the details of the connection's connection string at the information level.
        /// </summary>
        /// <param name="connection">The connection object.</param>
        /// <param name="logger">The logger to send the details to.</param>
        public static void LogConnectionStringAsInformation(this IDbConnection connection, ILogger logger)
        {
            connection.LogConnectionString(logger, LogLevel.Information);
        }

        /// <summary>
        /// Logs the details of the connection's connection string at the debug level.
        /// </summary>
        /// <param name="connection">The connection object.</param>
        /// <param name="logger">The logger to send the details to.</param>
        public static void LogConnectionStringAsDebug(this IDbConnection connection, ILogger logger)
        {
            connection.LogConnectionString(logger, LogLevel.Debug);
        }

        /// <summary>
        /// Logs the details of the connection's connection string at the trace level.
        /// </summary>
        /// <param name="connection">The connection object.</param>
        /// <param name="logger">The logger to send the details to.</param>
        public static void LogConnectionStringAsTrace(this IDbConnection connection, ILogger logger)
        {
            connection.LogConnectionString(logger, LogLevel.Trace);
        }

        /// <summary>
        /// Logs the deconstructed connection string at the desired level.
        /// </summary>
        /// <param name="logger">The logger to send the details to.</param>
        /// <param name="level">The level to log at.</param>
        /// <param name="connectionString">The connection string to be deconstructed.</param>
        /// <param name="connectionStringName">The name of the connection string, or null if there is no name or it is not known.</param>
        public static void LogConnectionString(this ILogger logger, LogLevel level, string connectionString, string connectionStringName = null)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                logger.Log(level, "No connection string to log.");
                return;
            }

            DbConnectionStringBuilder builder = new DbConnectionStringBuilder();
            builder.ConnectionString = connectionString;

            List<object> args = new List<object>();
            StringBuilder messageTemplate = new StringBuilder(connectionString.Length);
            BuildStartOfMessage(messageTemplate, connectionStringName, args);
            AddConnectionStringKeysAndValues(messageTemplate, builder, args);

            var objArgs = args.ToArray();
            logger.Log(level, messageTemplate.ToString(), objArgs);
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
            List<object> args)
        {
            var keys = builder.Keys ?? Array.Empty<string>();
            int index = 0;
            foreach (var key in keys)
            {
                messageTemplate.AppendLine(" * {key" + index + "} = {value" + index + "}");
                args.Add(key);
                args.Add(builder[(string) key]);
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