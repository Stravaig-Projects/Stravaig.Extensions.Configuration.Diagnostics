using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stravaig.Extensions.Logging.Diagnostics;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests
{
    public class TestBase
    {
        protected TestCaptureLoggerProvider CaptureLoggerProvider;
        protected ILogger Logger;
        protected IConfigurationRoot ConfigRoot;

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

        protected void SetupConfig(Action<IConfigurationBuilder> configure)
        {
            var builder = new ConfigurationBuilder();
            configure(builder);
            ConfigRoot = builder.Build();
        }

        protected IReadOnlyList<LogEntry> GetLogs()
        {
            return CaptureLoggerProvider.GetLogEntriesFor(GetType());
        }
    }
}