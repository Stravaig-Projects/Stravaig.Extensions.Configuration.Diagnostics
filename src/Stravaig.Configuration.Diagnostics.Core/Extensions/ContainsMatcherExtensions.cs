using System;
using Stravaig.Configuration.Diagnostics.FluentOptions;
using Stravaig.Configuration.Diagnostics.Matchers;

namespace Stravaig.Configuration.Diagnostics.Extensions
{
    /// <summary>
    /// Extension methods for adding the Contains matcher to the options builder.
    /// </summary>
    public static class ContainsMatcherExtensions
    {
        /// <summary>
        /// Adds a contains matcher to the options builder with the default comparison.
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <param name="contains">The string that the key contains.</param>
        /// <returns>The options builder.</returns>
        public static IHandOverToNewSectionOptionsBuilder Containing(
            this IKeyMatcherOptionsBuilder builder,
            string contains)
        {
           return builder.Matching(new ContainsMatcher(contains));
        }
        
        /// <summary>
        /// Adds a contains matcher to the options builder.
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <param name="contains">The string that the key contains.</param>
        /// <param name="comparison">The comparison options.</param>
        /// <returns>The options builder.</returns>
        public static IHandOverToNewSectionOptionsBuilder Containing(
            this IKeyMatcherOptionsBuilder builder,
            string contains,
            StringComparison comparison)
        {
            return builder.Matching(new ContainsMatcher(contains, comparison));
        }

        /// <summary>
        /// Adds a contains matcher to the options builder with the default comparison.
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <param name="contains">The string that the key contains.</param>
        /// <returns>The options builder.</returns>
        public static ISubsequentMultipleKeyMatcherOptionsBuilder KeyContains(
            this IFirstMultipleKeyMatcherOptionsBuilder builder,
            string contains)
        {
            return builder.KeyMatches(new ContainsMatcher(contains));
        }
        
        /// <summary>
        /// Adds a contains matcher to the options builder.
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <param name="contains">The string that the key contains.</param>
        /// <param name="comparison">The comparison options to use.</param>
        /// <returns>The options builder.</returns>
        public static ISubsequentMultipleKeyMatcherOptionsBuilder KeyContains(
            this IFirstMultipleKeyMatcherOptionsBuilder builder,
            string contains,
            StringComparison comparison)
        {
            return builder.KeyMatches(new ContainsMatcher(contains, comparison));
        }
        
        /// <summary>
        /// Adds a contains matcher to the options builder with the default comparison.
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <param name="contains">The string that the key contains.</param>
        /// <returns>The options builder.</returns>
        public static ISubsequentMultipleKeyMatcherOptionsBuilder OrContains(
            this ISubsequentMultipleKeyMatcherOptionsBuilder builder,
            string contains)
        {
            return builder.OrMatches(new ContainsMatcher(contains));
        }
        
        /// <summary>
        /// Adds a contains matcher to the options builder.
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <param name="contains">The string that the key contains.</param>
        /// <param name="comparison">The comparison options to use.</param>
        /// <returns>The options builder.</returns>
        public static ISubsequentMultipleKeyMatcherOptionsBuilder OrContains(
            this ISubsequentMultipleKeyMatcherOptionsBuilder builder,
            string contains,
            StringComparison comparison)
        {
            return builder.OrMatches(new ContainsMatcher(contains, comparison));
        }
    }
}