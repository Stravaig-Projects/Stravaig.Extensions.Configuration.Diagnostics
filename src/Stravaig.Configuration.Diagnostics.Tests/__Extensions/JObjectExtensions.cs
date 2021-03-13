using System;
using System.Collections.Generic;
using System.Linq;
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

        public static string GetMessageTemplate(this JObject logObject)
        {
            return logObject.Value<string>("MessageTemplate");
        }

        public static IReadOnlyList<KeyValuePair<string, string>> GetProperties(this JObject logObject)
        {
            var logProperties = logObject["Properties"]
                .Cast<JProperty>()
                .Select(p => new KeyValuePair<string, string>(p.Name, p.Value.Value<string>()))
                .ToArray();
            return logProperties;
        }

        public static string GetException(this JObject logObject)
        {
            return logObject.Value<string>("Exception");
        }
    }
}