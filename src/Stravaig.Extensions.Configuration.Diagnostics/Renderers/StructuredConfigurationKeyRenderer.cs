using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Stravaig.Extensions.Configuration.Diagnostics.Renderers
{
    public class StructuredConfigurationKeyRenderer : Renderer
    {
        public static readonly StructuredConfigurationKeyRenderer Instance = new StructuredConfigurationKeyRenderer();

        public MessageEntry Render(IConfiguration configuration, ConfigurationDiagnosticsOptions options)
        {
            var configurationValues = configuration.AsEnumerable()
                .OrderBy(kvp => kvp.Key)
                .Select(kvp => Obfuscate(kvp, options))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            
            List<object> args = new List<object>();
            StringBuilder messageTemplate = new StringBuilder("The following values are available:" + Environment.NewLine);
            
            foreach (var kvp in configurationValues)
            {
                var configurationKeyParts = kvp.Key.Split(':');
                var configurationKeyPlaceholder = Placeholder(configurationKeyParts);
                messageTemplate.AppendLine($"{kvp.Key} : {configurationKeyPlaceholder}");
                args.Add(kvp.Value);
            }
            
            var objArgs = args.ToArray();
            return new MessageEntry(messageTemplate.ToString(), objArgs);
        }
        
        private static KeyValuePair<string, string> Obfuscate(KeyValuePair<string, string> kvp, ConfigurationDiagnosticsOptions options)
        {
            return options.ConfigurationKeyMatcher.IsMatch(kvp.Key)
                ? new KeyValuePair<string, string>(kvp.Key, options.Obfuscator.Obfuscate(kvp.Value))
                : kvp;
        }
    }
}