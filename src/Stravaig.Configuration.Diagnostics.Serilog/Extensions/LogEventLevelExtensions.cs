using Serilog.Events;
using Stravaig.Configuration.Diagnostics.Renderers;

namespace Stravaig.Configuration.Diagnostics.Serilog.Extensions
{
    internal static class LogEventLevelExtensions
    {
        internal static MessageLevel ToStravaigMessageLevel(this LogEventLevel logEventLevel)
        {
            switch (logEventLevel)
            {
                case LogEventLevel.Fatal:
                    return MessageLevel.Critical;
                case LogEventLevel.Error:
                    return MessageLevel.Error;
                case LogEventLevel.Warning:
                    return MessageLevel.Warning;
                case LogEventLevel.Information:
                    return MessageLevel.Information;
                case LogEventLevel.Debug:
                    return MessageLevel.Debug;
                case LogEventLevel.Verbose:
                    return MessageLevel.Trace;
                default:
                    return MessageLevel.None;
            }
            
        }
    }
}