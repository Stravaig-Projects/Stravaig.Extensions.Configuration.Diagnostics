using Microsoft.Extensions.Logging;
using Stravaig.Configuration.Diagnostics.Renderers;

namespace Stravaig.Extensions.Configuration.Diagnostics
{
    internal static class MessageEntryExtensions
    {
        internal static LogLevel GetLogLevel(this MessageEntry entry, LogLevel logLevel)
        {
            return (LogLevel) entry.GetMessageLevel((MessageLevel) logLevel);
        }
    }
}