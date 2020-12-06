using System.Text.RegularExpressions;
using Stravaig.Extensions.Configuration.Diagnostics.Matchers;

namespace Stravaig.Extensions.Configuration.Diagnostics
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
        /// <returns>The key match builder.</returns>
        public static IKeyMatchBuilder AddContainsMatcher(this IKeyMatchBuilder builder, string contains)
        {
            return builder.Add(new ContainsMatcher(contains));
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