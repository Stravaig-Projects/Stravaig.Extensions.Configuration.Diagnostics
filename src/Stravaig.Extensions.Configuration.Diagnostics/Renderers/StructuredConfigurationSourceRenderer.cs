using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Stravaig.Extensions.Configuration.Diagnostics.Renderers
{
    public interface IConfigurationSourceRenderer
    {
        /// <summary>
        /// Renders the configuration source(s) for the given key for use by a logger.
        /// </summary>
        /// <param name="configRoot">The configuration root that contains all the providers.</param>
        /// <param name="key">The key to search for.</param>
        /// <param name="compressed">Whether to skip providers that have no value for this key, or render them against a null value.</param>
        /// <param name="options">The configuration diagnostics options</param>
        /// <returns></returns>
        MessageEntry Render(IConfigurationRoot configRoot, string key, bool compressed,
            ConfigurationDiagnosticsOptions options);
    }
    
    public class StructuredConfigurationSourceRenderer : Renderer, IConfigurationSourceRenderer
    {
        /// <summary>
        /// The single instance of this class.
        /// </summary>
        public static IConfigurationSourceRenderer Instance { get; } = new StructuredConfigurationSourceRenderer();
        
        /// <inheritdoc />
        public MessageEntry Render(IConfigurationRoot configRoot, string key, bool compressed,
            ConfigurationDiagnosticsOptions options)
        {
            if (configRoot.Providers.Any())
                return ReportProvidersWithKey(configRoot, key, compressed, options);

            return ReportNoProviders(key);
        }
        
        
        private MessageEntry ReportNoProviders(string configKey)
        {
            return new MessageEntry(
                $"Cannot track {Placeholder(nameof(configKey))}. No configuration providers found.",
                configKey);
        }

        private MessageEntry ReportProvidersWithKey(IConfigurationRoot configRoot, string configKey, bool compressed,
            ConfigurationDiagnosticsOptions options)
        {
            List<object> values = new List<object>();
            bool found = false;
            bool obfuscate = options.ConfigurationKeyMatcher.IsMatch(configKey);
            var report = new StringBuilder();
            report.Append($"Provider sources for value of configuration key {Placeholder("ConfigKey")}");
            values.Add(configKey);
            int index = 0;
            foreach (IConfigurationProvider provider in configRoot.Providers)
            {
                if (provider.TryGet(configKey, out string value))
                {
                    found = true;
                    report.AppendLine();
                    report.Append($"* {index}: {Placeholder(nameof(provider), index.ToString(CultureInfo.InvariantCulture))} ==> {Placeholder(nameof(provider), index.ToString(CultureInfo.InvariantCulture), nameof(value))}");
                    values.Add(provider.ToString());
                    if (obfuscate)
                        value = options.Obfuscator.Obfuscate(value);
                    values.Add(value);
                }
                else if (!compressed)
                {
                    report.AppendLine();
                    report.Append($"* {index}: {Placeholder(nameof(provider), index.ToString(CultureInfo.InvariantCulture))} ==> {Placeholder(nameof(provider), index.ToString(CultureInfo.InvariantCulture), nameof(value))}");
                    values.Add(provider.ToString());
                    values.Add(null);
                }

                index++;
            }

            if (!found)
            {
                if (compressed)
                    report.Append(" were not found.");
                else
                {
                    report.AppendLine();
                    report.Append("Key not found in any provider.");
                }
            }

            return new MessageEntry(report.ToString(), values.ToArray());
        }
    }
}