using Stravaig.Extensions.Configuration.Diagnostics.Matchers;

namespace Stravaig.Extensions.Configuration.Diagnostics.FluentOptions
{
    public interface ISubsequentMultipleKeyMatcherOptionsBuilder
    {
        ISubsequentMultipleKeyMatcherOptionsBuilder OrMatches(IMatcher matcher);
        ConfigurationDiagnosticsOptionsBuilder And { get; }
        IFinishBuildingOptions AndFinally { get; }
    }

    public partial class ConfigurationDiagnosticsOptionsBuilder
        : ISubsequentMultipleKeyMatcherOptionsBuilder
    {
        ISubsequentMultipleKeyMatcherOptionsBuilder ISubsequentMultipleKeyMatcherOptionsBuilder.OrMatches(IMatcher matcher)
        {
            _currentKeyMatcherBuilder.Add(matcher);
            return this;
        }

        ConfigurationDiagnosticsOptionsBuilder ISubsequentMultipleKeyMatcherOptionsBuilder.And => this;

        IFinishBuildingOptions ISubsequentMultipleKeyMatcherOptionsBuilder.AndFinally => this;
    }
}