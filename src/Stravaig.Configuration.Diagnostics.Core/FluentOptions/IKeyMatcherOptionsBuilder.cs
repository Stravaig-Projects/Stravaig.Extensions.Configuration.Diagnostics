using Stravaig.Configuration.Diagnostics.Matchers;

namespace Stravaig.Configuration.Diagnostics.FluentOptions
{
    /// <summary>
    /// The part of the fluent interface concerned with setting up the key matcher(s).
    /// </summary>
    public interface IKeyMatcherOptionsBuilder
    {
        /// <summary>
        /// Sets the matcher.
        /// </summary>
        /// <param name="matcher">The matcher to set.</param>
        /// <returns>The part of the fluent interface that hands over to a new section.</returns>
        IHandOverToNewSectionOptionsBuilder Matching(IMatcher matcher);

        /// <summary>
        /// Initiates setting multiple matchers.
        /// </summary>
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