using System.Text.RegularExpressions;
using Stravaig.Configuration.Diagnostics.FluentOptions;
using Stravaig.Configuration.Diagnostics.Matchers;

namespace Stravaig.Configuration.Diagnostics
{
    /// <summary>
    /// Extension methods for adding Regex key matchers to the configuration.
    /// </summary>
    public static class RegexMatcherExtensions
    {
        /// <summary>
        /// Matches the key on the given regular expression pattern with the default options.
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <returns>The options builder.</returns>
        public static IHandOverToNewSectionOptionsBuilder MatchingPattern(
            this IKeyMatcherOptionsBuilder builder,
            string regexPattern)
        {
            return builder.Matching(new RegexMatcher(regexPattern));
        }
        
        /// <summary>
        /// Matches the key on the given regular expression pattern with the default options.
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <param name="options">The options to modify the matching with.</param>
        /// <returns>The options builder.</returns>
        public static IHandOverToNewSectionOptionsBuilder MatchingPattern(
            this IKeyMatcherOptionsBuilder builder,
            string regexPattern,
            RegexOptions options)
        {
            return builder.Matching(new RegexMatcher(regexPattern, options));
        }
        
        /// <summary>
        /// Matches the key on the given regular expression pattern with the default options.
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <returns>The options builder.</returns>
        public static ISubsequentMultipleKeyMatcherOptionsBuilder KeyMatchesPattern(
            this IFirstMultipleKeyMatcherOptionsBuilder builder,
            string regexPattern)
        {
            return builder.KeyMatches(new RegexMatcher(regexPattern));
        }

        /// <summary>
        /// Matches the key on the given regular expression pattern with the default options.
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <param name="options">The options to modify the matching process with.</param>
        /// <returns>The options builder.</returns>
        public static ISubsequentMultipleKeyMatcherOptionsBuilder KeyMatchesPattern(
            this IFirstMultipleKeyMatcherOptionsBuilder builder,
            string regexPattern,
            RegexOptions options)
        {
            return builder.KeyMatches(new RegexMatcher(regexPattern, options));
        }

        /// <summary>
        /// Matches the key on the given regular expression pattern with the default options.
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <returns>The options builder.</returns>
        public static ISubsequentMultipleKeyMatcherOptionsBuilder OrMatchesPattern(
            this ISubsequentMultipleKeyMatcherOptionsBuilder builder,
            string regexPattern)
        {
            return builder.OrMatches(new RegexMatcher(regexPattern));
        }
        
        /// <summary>
        /// Matches the key on the given regular expression pattern with the default options.
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <param name="options">The options to modify the matching process with.</param>
        /// <returns>The options builder.</returns>
        public static ISubsequentMultipleKeyMatcherOptionsBuilder OrMatchesPattern(
            this ISubsequentMultipleKeyMatcherOptionsBuilder builder,
            string regexPattern,
            RegexOptions options)
        {
            return builder.OrMatches(new RegexMatcher(regexPattern, options));
        }
    }
}