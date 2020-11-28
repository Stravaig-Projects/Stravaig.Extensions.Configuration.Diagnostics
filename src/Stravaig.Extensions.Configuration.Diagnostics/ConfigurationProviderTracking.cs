using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Stravaig.Extensions.Configuration.Diagnostics
{
    public static class ConfigurationProviderTracking
    {
        public static void LogConfigurationKeySource(this ILogger logger, LogLevel level, IConfigurationRoot configRoot, string key, bool compressed = false, ConfigurationDiagnosticsOptions options = null)
        {
            if (options == null)
                options = ConfigurationDiagnosticsOptions.GlobalOptions;

            bool obfuscate = options.ConfigurationKeyMatcher.IsMatch(key);

            StringBuilder report = new StringBuilder();
            report.Append("Source of value for ");
            report.Append(key);
            foreach (IConfigurationProvider provider in configRoot.Providers)
            {
                if (provider.TryGet(key, out string value))
                {
                    report.AppendLine();
                    report.Append("* ");
                    report.Append(provider);
                    report.Append(" ==> ");
                    if (obfuscate)
                    {
                        value = options.Obfuscator.Obfuscate(value);
                        report.Append(value);
                    }
                    else
                    {
                        report.Append('"');
                        report.Append(value);
                        report.Append('"');
                    }

                }
                else if (!compressed)
                {
                    report.AppendLine();
                    report.Append("* ");
                    report.Append(provider);
                    report.Append(" ==> ");
                    report.Append("null");
                }
            }

            logger.Log(level, report.ToString());
        }
    }
}