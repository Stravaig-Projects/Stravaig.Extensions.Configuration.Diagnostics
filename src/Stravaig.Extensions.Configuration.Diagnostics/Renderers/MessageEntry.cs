using System;
using Microsoft.Extensions.Logging;

namespace Stravaig.Extensions.Configuration.Diagnostics.Renderers
{
    public class MessageEntry
    {
        public string MessageTemplate { get; private set; }
        public object[] Properties { get; }
        public Exception Exception { get; }

        public MessageEntry(string messageTemplate, params object[] properties)
        {
            MessageTemplate = messageTemplate;
            Properties = properties;
            Exception = null;
        }
        
        public MessageEntry(Exception exception, string messageTemplate, params object[] properties)
        {
            MessageTemplate = messageTemplate;
            Properties = properties;
            Exception = exception;
        }

        public LogLevel GetLogLevel(LogLevel requestedLevel)
        {
            if (Exception == null)
                return requestedLevel;
            var warningOrGreater = (LogLevel) Math.Max((int) requestedLevel, (int) LogLevel.Warning);
            return warningOrGreater;
        }

        public bool HasException => Exception != null;
    }
}