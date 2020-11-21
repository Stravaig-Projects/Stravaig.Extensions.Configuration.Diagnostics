using Stravaig.Extensions.Configuration.Diagnostics.Matchers;

namespace Stravaig.Extensions.Configuration.Diagnostics.FluentOptions
{
    public interface IKeyMatcherOptionsBuilder
    {
        IHandOverToNewSectionOptionsBuilder Matching(IMatcher matcher);
        IFirstMultipleKeyMatcherOptionsBuilder Where { get; }
    }

    public partial class ConfigurationDiagnosticsOptionsBuilder
        : IKeyMatcherOptionsBuilder
    {
        IHandOverToNewSectionOptionsBuilder IKeyMatcherOptionsBuilder.Matching(IMatcher matcher)
        {
            _currentKeyMatcherBuilder.Add(matcher);
            return this;
        }

        IFirstMultipleKeyMatcherOptionsBuilder IKeyMatcherOptionsBuilder.Where => this;
    }
}