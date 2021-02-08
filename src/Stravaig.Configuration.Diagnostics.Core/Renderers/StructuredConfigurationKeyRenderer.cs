using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Stravaig.Configuration.Diagnostics.Renderers
{
    /// <summary>
    /// A structured renderer that renders all the configuration values in a configuration.
    /// </summary>
    public class StructuredConfigurationKeyRenderer : Renderer, IConfigurationKeyRenderer
    {
        /// <summary>
        /// The single instance of this class.
        /// </summary>
        public static readonly StructuredConfigurationKeyRenderer Instance = new StructuredConfigurationKeyRenderer();

        /// <inheritdoc />
        public MessageEntry Render(IConfiguration configuration, ConfigurationDiagnosticsOptions options)
        {
            List<object> args = new List<object>();
            StringBuilder messageTemplate = new StringBuilder("The following values are available:" + Environment.NewLine);
            
            foreach (var kvp in configuration.AsEnumerable())
            {
                var configurationKeyPlaceholder = Placeholder(kvp.Key);
                var potentiallyObfuscatedValue = Obfuscate(kvp, options);
                
                messageTemplate.AppendLine($"{kvp.Key} : {configurationKeyPlaceholder}");
                args.Add(potentiallyObfuscatedValue);
            }
            
            var objArgs = args.ToArray();
            return new MessageEntry(messageTemplate.ToString(), objArgs);
        }
        
        private static string Obfuscate(KeyValuePair<string, string> kvp, ConfigurationDiagnosticsOptions options)
        {
            return options.ConfigurationKeyMatcher.IsMatch(kvp.Key)
                ? options.Obfuscator.Obfuscate(kvp.Value)
                : kvp.Value;
        }
    }
}