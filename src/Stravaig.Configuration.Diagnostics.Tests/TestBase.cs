using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework.Internal;
using Stravaig.Extensions.Logging.Diagnostics;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests
{
    public class LoggerExtensionsBase : TestBase
    {
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
        
    }
    public class TestBase
    {
        protected TestCaptureLoggerProvider CaptureLoggerProvider;
        protected IConfigurationRoot ConfigRoot;


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