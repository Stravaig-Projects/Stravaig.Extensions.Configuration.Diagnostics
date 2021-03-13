using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Stravaig.Extensions.Logging.Diagnostics;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests
{
    public abstract class LoggerExtensionsTestBase : TestBase
    {
        protected TestCaptureLoggerProvider CaptureLoggerProvider;
        protected ILogger Logger;
        protected void SetupLogger()
        {
            CaptureLoggerProvider = new TestCaptureLoggerProvider();
            var loggerFactory = LoggerFactory.Create(b =>
            {
                b.AddConsole(o =>
                {
                    o.DisableColors = true;
                });
                b.AddProvider(CaptureLoggerProvider);
                b.SetMinimumLevel(LogLevel.Trace);
            });
            Logger = loggerFactory.CreateLogger(GetType());
        }
        
        protected IReadOnlyList<LogEntry> GetLogs()
        {
            return CaptureLoggerProvider.GetLogEntriesFor(GetType());
        }
    }
}