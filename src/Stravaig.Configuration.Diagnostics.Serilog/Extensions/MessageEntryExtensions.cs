using Stravaig.Configuration.Diagnostics.Renderers;
using Serilog.Events;

namespace Stravaig.Configuration.Diagnostics.Serilog.Extensions
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