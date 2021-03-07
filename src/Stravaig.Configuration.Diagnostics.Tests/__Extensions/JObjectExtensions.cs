using System;
using Newtonsoft.Json.Linq;
using Serilog.Events;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.__Extensions
{
    public static class JObjectExtensions
    {
        public static LogEventLevel? GetLevel(this JObject logObject)
        {
            string level = logObject.Value<string>("Level");
            if (Enum.TryParse<LogEventLevel>(level, out var result))
                return result;

            return null;
        }

        public static string GetMessage(this JObject logObject)
        {
            return logObject.Value<string>("RenderedMessage");
        }
    }
}