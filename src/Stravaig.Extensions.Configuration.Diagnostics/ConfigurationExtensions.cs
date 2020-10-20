using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Stravaig.Extensions.Configuration.Diagnostics
{
    public static class ConfigurationExtensions
    {
        public static void LogConfigurationValuesAsInformation(this IConfiguration config, ILogger logger)
        {
            config.LogConfigurationValues(logger, LogLevel.Information);
        }

        public static void LogConfigurationValuesAsDebug(this IConfiguration config, ILogger logger)
        {
            config.LogConfigurationValues(logger, LogLevel.Debug);
        }

        public static void LogConfigurationValuesAsTrace(this IConfiguration config, ILogger logger)
        {
            config.LogConfigurationValues(logger, LogLevel.Trace);
        }
        
        public static void LogConfigurationValues(this IConfiguration config, ILogger logger, LogLevel level)
        {
            var valueMap = config.AsEnumerable()
                .OrderBy(kvp => kvp.Key)
                .Select(kvp => $"{kvp.Key} : {kvp.Value}");
            string message = "The following values are available:" +
                             Environment.NewLine +
                             string.Join(Environment.NewLine, valueMap);
            
            logger.Log(level, message);
        }
    }
}