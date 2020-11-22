using System;
using Stravaig.Extensions.Configuration.Diagnostics.FluentOptions;
using Stravaig.Extensions.Configuration.Diagnostics.Matchers;

namespace Stravaig.Extensions.Configuration.Diagnostics
{
    public static class ContainsMatcherExtensions
    {
        public static IHandOverToNewSectionOptionsBuilder Containing(
            this IKeyMatcherOptionsBuilder builder,
            string contains)
        {
           return builder.Matching(new ContainsMatcher(contains));
        }
        
        public static IHandOverToNewSectionOptionsBuilder Containing(
            this IKeyMatcherOptionsBuilder builder,
            string contains,
            StringComparison comparison)
        {
            return builder.Matching(new ContainsMatcher(contains, comparison));
        }

        public static ISubsequentMultipleKeyMatcherOptionsBuilder KeyContains(
            this IFirstMultipleKeyMatcherOptionsBuilder builder,
            string contains)
        {
            return builder.KeyMatches(new ContainsMatcher(contains));
        }
        
        public static ISubsequentMultipleKeyMatcherOptionsBuilder KeyContains(
            this IFirstMultipleKeyMatcherOptionsBuilder builder,
            string contains,
            StringComparison comparison)
        {
            return builder.KeyMatches(new ContainsMatcher(contains, comparison));
        }
        
        public static ISubsequentMultipleKeyMatcherOptionsBuilder OrContains(
            this ISubsequentMultipleKeyMatcherOptionsBuilder builder,
            string contains)
        {
            return builder.OrMatches(new ContainsMatcher(contains));
        }
        
        public static ISubsequentMultipleKeyMatcherOptionsBuilder OrContains(
            this ISubsequentMultipleKeyMatcherOptionsBuilder builder,
            string contains,
            StringComparison comparison)
        {
            return builder.OrMatches(new ContainsMatcher(contains, comparison));
        }
    }
}