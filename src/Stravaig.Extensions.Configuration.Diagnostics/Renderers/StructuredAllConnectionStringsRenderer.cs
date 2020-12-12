using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Stravaig.Extensions.Configuration.Diagnostics.Renderers
{
    public class StructuredAllConnectionStringsRenderer: Renderer, IAllConnectionStringsRenderer
    {
        public static readonly StructuredAllConnectionStringsRenderer Instance = new StructuredAllConnectionStringsRenderer();
        
        public MessageEntry Render(IConfiguration config, ConfigurationDiagnosticsOptions options)
        {
            var connectionStringSection = config.GetSection("ConnectionStrings");
            var names = connectionStringSection
                .GetChildren()
                .Select(s => s.Key)
                .OrderBy(k => k)
                .ToArray();

            if (names.Length == 0)
                return new MessageEntry("No connections strings found in the configuration.");

            List<Exception> exceptions = new List<Exception>();
            List<object> properties = new List<object>(names);
            StringBuilder messageTemplate = new StringBuilder();
            messageTemplate.Append("The following connection strings were found: ");
            messageTemplate.Append(string.Join(", ", names
                .Select(name => Placeholder(name, nameof(name)))));
            messageTemplate.AppendLine(".");
            
            foreach (string name in names)
            {
                var connectionString = config.GetConnectionString(name);

                var message = StructuredConnectionStringRenderer.Instance.Render(connectionString, name, options);
                messageTemplate.AppendLine(message.MessageTemplate);
                properties.AddRange(message.Properties);

                if (message.HasException)
                    exceptions.Add(message.Exception);
            }

            switch (exceptions.Count)
            {
                case 0:
                    return new MessageEntry(messageTemplate.ToString(), properties.ToArray());
                case 1:
                    return new MessageEntry(exceptions[0], messageTemplate.ToString(), properties.ToArray());
                default:
                    return new MessageEntry(
                        new AggregateException("Multiple errors occurred when rendering the connection strings.", exceptions),
                        messageTemplate.ToString(),
                        properties);
            }
        }
    }
}