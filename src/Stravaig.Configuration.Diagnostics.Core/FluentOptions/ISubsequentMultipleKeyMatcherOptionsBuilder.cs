using Stravaig.Configuration.Diagnostics.Matchers;

namespace Stravaig.Configuration.Diagnostics.FluentOptions
{
    /// <summary>
    /// The fluent interface for setting up additional key matchers, or moving on to another part of the fluent configuration.
    /// </summary>
    public interface ISubsequentMultipleKeyMatcherOptionsBuilder
    {
        /// <summary>
        /// Adds a subsequent matcher.
        /// </summary>
        /// <param name="matcher">The additional matcher.</param>
        /// <returns>The fluent interface for setting up additional key matchers, or moving on to another part of the fluent configuration.</returns>
        ISubsequentMultipleKeyMatcherOptionsBuilder OrMatches(IMatcher matcher);

        /// <summary>
        /// Move on to another part of the fluent options configuration.
        /// </summary>
        ConfigurationDiagnosticsOptionsBuilder And { get; }
        
        /// <summary>
        /// Initiate the resolution of the fluent options configuration.
        /// </summary>
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