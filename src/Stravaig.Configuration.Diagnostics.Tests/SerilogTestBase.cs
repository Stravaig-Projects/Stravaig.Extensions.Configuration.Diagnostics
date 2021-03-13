using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Serilog;
using Serilog.Formatting.Json;
using ILogger = Serilog.ILogger;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests
{
    public abstract class SerilogTestBase : TestBase
    {
        protected ILogger Logger;
        private StringBuilder _logStringBuilder;
        private TextWriter _logTextWriter;
        protected void SetupLogger()
        {
            _logStringBuilder = new StringBuilder();
            _logTextWriter = new StringWriter(_logStringBuilder);
            Logger = new LoggerConfiguration()
                .WriteTo.TextWriter(new JsonFormatter(renderMessage: true), _logTextWriter)
                .MinimumLevel.Verbose()
                .CreateLogger();
        }


        public IReadOnlyList<JObject> GetLogEntriesFor<T>()
        {
            return GetLogs(typeof(T));
        }

        public IReadOnlyList<JObject> GetLogs(Type type = null)
        {
            using StringReader reader = new StringReader(_logStringBuilder.ToString());
            return GetLogEntries(reader, type).ToArray();
        }
        
        private IEnumerable<JObject> GetLogEntries(TextReader reader, Type filter)
        {
            while(reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                JObject entry = JObject.Parse(line);
                if (filter == null)
                    yield return entry;
                else if (entry["SourceContext"].Value<string>() == filter.FullName)
                    yield return entry;
            }
        }

        [TearDown]
        public virtual void TearDown()
        {
            _logTextWriter?.Dispose();
            _logTextWriter = null;
        }
        
    }
}