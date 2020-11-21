using Stravaig.Extensions.Configuration.Diagnostics.Matchers;

namespace Stravaig.Extensions.Configuration.Diagnostics.FluentOptions
{
    public interface IFirstMultipleKeyMatcherOptionsBuilder
    {
        ISubsequentMultipleKeyMatcherOptionsBuilder KeyMatches(IMatcher matcher);
    }

    public partial class ConfigurationDiagnosticsOptionsBuilder
        : IFirstMultipleKeyMatcherOptionsBuilder
    {
        ISubsequentMultipleKeyMatcherOptionsBuilder IFirstMultipleKeyMatcherOptionsBuilder.KeyMatches(IMatcher matcher)
        {
            _currentKeyMatcherBuilder.Add(matcher);
            return this;
        }
    }
}