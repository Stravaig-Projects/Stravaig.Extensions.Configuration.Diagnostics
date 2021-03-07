using System.Collections;
using Serilog.Events;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.__data
{
    public class LogEventLevelSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            return new[]
            {
                LogEventLevel.Verbose,
                LogEventLevel.Debug,
                LogEventLevel.Information,
                LogEventLevel.Warning,
                LogEventLevel.Error,
                LogEventLevel.Fatal,
            }.GetEnumerator();
        }
    }
}