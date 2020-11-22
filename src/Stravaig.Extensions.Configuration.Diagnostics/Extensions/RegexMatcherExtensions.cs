using System.Text.RegularExpressions;
using Stravaig.Extensions.Configuration.Diagnostics.FluentOptions;
using Stravaig.Extensions.Configuration.Diagnostics.Matchers;

namespace Stravaig.Extensions.Configuration.Diagnostics
{
    public static class RegexMatcherExtensions
    {
        public static IHandOverToNewSectionOptionsBuilder MatchingPattern(
            this IKeyMatcherOptionsBuilder builder,
            string regexPattern)
        {
            return builder.Matching(new RegexMatcher(regexPattern));
        }
        
        public static IHandOverToNewSectionOptionsBuilder MatchingPattern(
            this IKeyMatcherOptionsBuilder builder,
            string regexPattern,
            RegexOptions options)
        {
            return builder.Matching(new RegexMatcher(regexPattern, options));
        }
        
        public static ISubsequentMultipleKeyMatcherOptionsBuilder KeyMatchesPattern(
            this IFirstMultipleKeyMatcherOptionsBuilder builder,
            string regexPattern)
        {
            return builder.KeyMatches(new RegexMatcher(regexPattern));
        }

        public static ISubsequentMultipleKeyMatcherOptionsBuilder KeyMatchesPattern(
            this IFirstMultipleKeyMatcherOptionsBuilder builder,
            string regexPattern,
            RegexOptions options)
        {
            return builder.KeyMatches(new RegexMatcher(regexPattern, options));
        }

        public static ISubsequentMultipleKeyMatcherOptionsBuilder OrMatchesPattern(
            this ISubsequentMultipleKeyMatcherOptionsBuilder builder,
            string regexPattern)
        {
            return builder.OrMatches(new RegexMatcher(regexPattern));
        }
        
        public static ISubsequentMultipleKeyMatcherOptionsBuilder OrMatchesPattern(
            this ISubsequentMultipleKeyMatcherOptionsBuilder builder,
            string regexPattern,
            RegexOptions options)
        {
            return builder.OrMatches(new RegexMatcher(regexPattern, options));
        }
    }
}