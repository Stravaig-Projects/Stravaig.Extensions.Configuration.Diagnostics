using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Stravaig.Configuration.Diagnostics.Matchers;

namespace Stravaig.Configuration.Diagnostics
{
    /// <summary>
    /// Extensions methods for the key matcher builder.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class IKeyMatchBuilderExtensions
    {
        /// <summary>
        /// Adds a contains matcher to the builder.
        /// </summary>
        /// <param name="builder">The key match builder.</param>
        /// <param name="contains">The contains matcher.</param>
        /// <param name="comparison">The method of comparing strings, defaults to OrdinalIgnoreCase</param>
        /// <returns>The key match builder.</returns>
        public static IKeyMatchBuilder AddContainsMatcher(this IKeyMatchBuilder builder, string contains, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return builder.Add(new ContainsMatcher(contains, comparison));
        }
        
        /// <summary>
        /// Adds a contains matcher to the builder.
        /// </summary>
        /// <param name="builder">The key match builder.</param>
        /// <param name="contains">The contains matcher.</param>
        /// <param name="comparison">The method of comparing strings, defaults to OrdinalIgnoreCase</param>
        /// <returns>The key match builder.</returns>
        public static IKeyMatchBuilder AddContainsMatcher(this IKeyMatchBuilder builder, IEnumerable<string> contains, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return builder.Add(new ContainsMatcher(contains, comparison));
        }

        /// <summary>
        /// Adds a regular expression matcher to the builder.
        /// </summary>
        /// <param name="builder">The key match builder.</param>
        /// <param name="regularExpressionPattern">The regular expression pattern to match with.</param>
        /// <returns>The key match builder.</returns>
        public static IKeyMatchBuilder AddRegexMatcher(this IKeyMatchBuilder builder, string regularExpressionPattern)
        {
            return builder.Add(new RegexMatcher(regularExpressionPattern));
        }

        /// <summary>
        /// Adds a regular expression matcher to the builder.
        /// </summary>
        /// <param name="builder">The key match builder.</param>
        /// <param name="regularExpressionPattern">The regular expression pattern to match with.</param>
        /// <param name="options">The options to modify the match with.</param>
        /// <returns>The key match builder.</returns>
        public static IKeyMatchBuilder AddRegexMatcher(this IKeyMatchBuilder builder, string regularExpressionPattern,
            RegexOptions options)
        {
            return builder.Add(new RegexMatcher(regularExpressionPattern, options));
        }
    }
}