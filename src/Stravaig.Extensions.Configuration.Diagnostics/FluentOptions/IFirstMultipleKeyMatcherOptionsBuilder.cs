using Stravaig.Extensions.Configuration.Diagnostics.Matchers;

namespace Stravaig.Extensions.Configuration.Diagnostics.FluentOptions
{
    /// <summary>
    /// The fluent interface for setting up the first of multiple key matchers.
    /// </summary>
    public interface IFirstMultipleKeyMatcherOptionsBuilder
    {
        /// <summary>
        /// Sets the key matcher to the given matcher.
        /// </summary>
        /// <param name="matcher">The matcher that determines if the key should be obfuscated.</param>
        /// <returns>The builder continuation to add additional matchers.</returns>
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