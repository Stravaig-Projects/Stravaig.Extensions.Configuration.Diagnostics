using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Stravaig.Extensions.Configuration.Diagnostics.Renderers
{
    /// <summary>
    /// Renders connection strings.
    /// </summary>
    public class StructuredConnectionStringRenderer : Renderer, IConnectionStringRenderer
    {
        /// <summary>
        /// The single instance of this class
        /// </summary>
        public static readonly StructuredConnectionStringRenderer Instance = new StructuredConnectionStringRenderer();

        /// <inheritdoc />
        public MessageEntry Render(string connectionString, string name, ConfigurationDiagnosticsOptions options)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                return NoConnectionString(name);
            
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
                return LogNotAValidConnectionString(name, ex);
            }

            List<object> args = new List<object>();
            StringBuilder messageTemplate = new StringBuilder(connectionString.Length);
            BuildStartOfMessage(messageTemplate, name, args);
            AddConnectionStringKeysAndValues(messageTemplate, builder, name, args, options);

            var objArgs = args.ToArray();
            return new MessageEntry(messageTemplate.ToString(), objArgs);
        }

        private MessageEntry NoConnectionString(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new MessageEntry("No connection string to log.");
            
            return new MessageEntry($"No connection string to log for {Placeholder(name, nameof(name))}.", name);
        }

        private MessageEntry LogNotAValidConnectionString(string name, Exception exception)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("The ");
            if (name != null)
                sb.Append($"\"{Placeholder(name, nameof(name))}\" ");
            sb.Append("connection string value could not be interpreted as a connection string.");
            return new MessageEntry(exception, sb.ToString(), name);
        }
        
        private void BuildStartOfMessage(
            StringBuilder messageTemplate,
            string name,
            List<object> args)
        {
            messageTemplate.Append("Connection string ");
            if (name != null)
            {
                messageTemplate.Append($"named ({Placeholder(name, nameof(name))}) ");
                args.Add(name);
            }

            messageTemplate.AppendLine("parameters:");
        }

        private void AddConnectionStringKeysAndValues(
            StringBuilder messageTemplate,
            DbConnectionStringBuilder builder,
            string name,
            List<object> args,
            ConfigurationDiagnosticsOptions options)
        {
            var keys = builder.Keys ?? Array.Empty<string>();
            foreach (var keyObj in keys)
            {
                string key = (string) keyObj;
                var value =
                    options.ConnectionStringElementMatcher.IsMatch(key)
                        ? options.Obfuscator.Obfuscate((string) builder[key])
                        : (string) builder[key];
 
                messageTemplate.AppendLine($" * {Placeholder(name, key, nameof(key))} = {Placeholder(name, key, nameof(value))}");
                args.Add(key);
                args.Add(value);
            }
        }
    }
}