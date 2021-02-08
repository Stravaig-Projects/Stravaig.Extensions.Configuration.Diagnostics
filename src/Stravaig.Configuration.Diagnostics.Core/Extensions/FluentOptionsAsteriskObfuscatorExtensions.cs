using Stravaig.Configuration.Diagnostics.FluentOptions;
using Stravaig.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Configuration.Diagnostics.Extensions
{
    /// <summary>
    /// Extensions to the fluent options builder for adding obfuscators.
    /// </summary>
    public static class FluentOptionsAsteriskObfuscatorExtensions
    {
        /// <summary>
        /// Add an obfuscator that replaces secrets with asterisks.
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <returns>The options builder.</returns>
        public static IHandOverToNewSectionOptionsBuilder WithAsterisks(this IObfuscatorOptionsBuilder builder)
        {
            return builder.With(new MatchedLengthAsteriskObfuscator());
        }

        /// <summary>
        /// Add an obfuscator that replaces secrets with a fixed length of asterisks.
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <returns>The options builder.</returns>
        public static IHandOverToNewSectionOptionsBuilder WithFixedAsterisks(this IObfuscatorOptionsBuilder builder)
        {
            return builder.With(new FixedAsteriskObfuscator());
        }

        /// <summary>
        /// Add an obfuscator that replaces secrets with a fixed length of asterisks.
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <param name="numAsterisks">The number of asterisks to use.</param>
        /// <returns>The options builder.</returns>
        public static IHandOverToNewSectionOptionsBuilder WithFixedAsterisks(this IObfuscatorOptionsBuilder builder, int numAsterisks)
        {
            return builder.With(new FixedAsteriskObfuscator(numAsterisks));
        }
    }
}