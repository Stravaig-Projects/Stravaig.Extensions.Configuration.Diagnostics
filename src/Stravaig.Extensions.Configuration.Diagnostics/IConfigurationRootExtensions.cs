using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Stravaig.Extensions.Configuration.Diagnostics
{
    public static class IConfigurationRootExtensions
    {
        public static void LogProviderNamesAsInformation(this IConfigurationRoot config, ILogger logger)
        {
            config.LogProviderNames(logger, LogLevel.Information);
        }

        public static void LogProviderNamesAsDebug(this IConfigurationRoot config, ILogger logger)
        {
            config.LogProviderNames(logger, LogLevel.Debug);
        }

        public static void LogProviderNamesAsTrace(this IConfigurationRoot config, ILogger logger)
        {
            config.LogProviderNames(logger, LogLevel.Trace);
        }

        public static void LogProviderNames(this IConfigurationRoot config, ILogger logger, LogLevel level)
        {
            var providerNames = config.Providers.Select(p => p.GetType().FullName);
            string message = "The following configuration providers were registered:" + Environment.NewLine
                + string.Join(Environment.NewLine, providerNames);
            logger.Log(level, message);
        }
    }
}