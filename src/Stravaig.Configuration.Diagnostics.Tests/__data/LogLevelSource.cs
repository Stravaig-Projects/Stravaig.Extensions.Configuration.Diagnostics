using System.Collections;
using Microsoft.Extensions.Logging;

namespace Stravaig.Extensions.Configuration.Diagnostics.Tests.__data
{
    public class LogLevelSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            return new[]
            {
                LogLevel.Trace,
                LogLevel.Debug,
                LogLevel.Information,
                LogLevel.Warning,
                LogLevel.Error,
                LogLevel.Critical,
            }.GetEnumerator();
        }
    }
}