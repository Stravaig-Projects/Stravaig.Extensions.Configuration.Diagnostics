using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
#if NET5_0_OR_GREATER
using Microsoft.Extensions.Logging.Console;
#endif
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
#if NETCOREAPP3_1
                b.AddConsole(o =>
                {
                    o.DisableColors = true;
                });
#elif NET5_0_OR_GREATER
                b.AddSimpleConsole(o =>
                {
                    o.ColorBehavior = LoggerColorBehavior.Disabled;
                });
#else
                Assert.Fail($"Unexpected target framework. Available target frameworks: {PreprocessorSymbols.StringList}");
#endif
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