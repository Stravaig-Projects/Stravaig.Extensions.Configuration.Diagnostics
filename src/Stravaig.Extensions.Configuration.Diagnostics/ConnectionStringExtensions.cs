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

        public static void LogConnectionString(this IDbConnection connection, ILogger logger, LogLevel level)
        {
            var connectionString = connection.ConnectionString;
            logger.LogConnectionString(level, connectionString);
        }

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