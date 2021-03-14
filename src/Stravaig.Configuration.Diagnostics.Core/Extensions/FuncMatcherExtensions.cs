using System;
using Stravaig.Configuration.Diagnostics.FluentOptions;
using Stravaig.Configuration.Diagnostics.Matchers;

namespace Stravaig.Configuration.Diagnostics
{
    /// <summary>
    /// Extension methods for adding the Func Matcher to the options builder.
    /// </summary>
    public static class FuncMatcherExtensions
    {
        /// <summary>
        /// Adds a function predicate to the options builder.
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <param name="predicate">The function predicate that determines if a key matches.</param>
        /// <returns>The options builder</returns>
        public static IHandOverToNewSectionOptionsBuilder Matching(
            this IKeyMatcherOptionsBuilder builder,
            Func<string, bool> predicate)
        {
            return builder.Matching(new FuncMatcher(predicate));
        }

        /// <summary>
        /// Adds a function predicate to the options builder.
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <param name="predicate">The function predicate that determines is a key matches.</param>
        /// <returns>The options builder.</returns>
        public static ISubsequentMultipleKeyMatcherOptionsBuilder KeyMatches(
            this IFirstMultipleKeyMatcherOptionsBuilder builder,
            Func<string, bool> predicate)
        {
            return builder.KeyMatches(new FuncMatcher(predicate));
        }

        /// <summary>
        /// Adds a function predicate to the options builder.
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <param name="predicate">The function predicate that determines is a key matches.</param>
        /// <returns>The options builder.</returns>
        public static ISubsequentMultipleKeyMatcherOptionsBuilder OrMatches(
            this ISubsequentMultipleKeyMatcherOptionsBuilder builder,
            Func<string, bool> predicate)
        {
            return builder.OrMatches(new FuncMatcher(predicate));
        }
    }
}