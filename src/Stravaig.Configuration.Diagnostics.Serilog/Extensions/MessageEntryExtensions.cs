using Serilog.Events;
using Stravaig.Configuration.Diagnostics.Renderers;

namespace Stravaig.Configuration.Diagnostics.Serilog
{
    internal static class MessageEntryExtensions
    {
        internal static LogEventLevel GetLogLevel(this MessageEntry entry, LogEventLevel logLevel)
        {
            MessageLevel messageLevel = logLevel.ToStravaigMessageLevel();
            return entry.GetMessageLevel(messageLevel)
                .ToSerilogLogEventLevel();
        }
    }
}