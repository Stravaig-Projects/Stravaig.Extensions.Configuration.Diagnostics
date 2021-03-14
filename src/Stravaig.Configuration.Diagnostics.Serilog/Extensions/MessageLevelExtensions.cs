using Serilog.Events;
using Stravaig.Configuration.Diagnostics.Renderers;

namespace Stravaig.Configuration.Diagnostics.Serilog
{
    internal static class MessageLevelExtensions
    {
        internal static LogEventLevel ToSerilogLogEventLevel(this MessageLevel messageLevel)
        {
            switch (messageLevel)
            {
                case MessageLevel.Critical:
                    return LogEventLevel.Fatal;
                case MessageLevel.Error:
                    return LogEventLevel.Error;
                case MessageLevel.Warning:
                    return LogEventLevel.Warning;
                case MessageLevel.Information:
                    return LogEventLevel.Information;
                case MessageLevel.Debug:
                    return LogEventLevel.Debug;
                case MessageLevel.Trace:
                    return LogEventLevel.Verbose;
                // ReSharper disable once RedundantCaseLabel
                case MessageLevel.None: // No available mapping.
                default:
                    return (LogEventLevel) int.MaxValue;
            }
        }
    }
}