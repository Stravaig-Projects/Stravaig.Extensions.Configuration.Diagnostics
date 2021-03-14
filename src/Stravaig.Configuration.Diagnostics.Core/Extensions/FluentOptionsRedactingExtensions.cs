using Stravaig.Configuration.Diagnostics.FluentOptions;
using Stravaig.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Configuration.Diagnostics
{
    /// <summary>
    /// Extension methods for obfuscating by redacting.
    /// </summary>
    public static class FluentOptionsRedactingExtensions
    {
        /// <summary>
        /// Add an obfuscator that replaces a secret with "REDACTED"
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <returns>The options builder</returns>
        public static IHandOverToNewSectionOptionsBuilder ByRedacting(this IObfuscatorOptionsBuilder builder)
        {
            return builder.With(new RedactedObfuscator());
        }

        /// <summary>
        /// Add an obfuscator that replaces a secret with an ornamented "REDACTED". 
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <param name="symmetricalAccoutrement">The string to adorn the obfuscated secret before and after the word "REDACTED"</param>
        /// <returns>The options builder</returns>
        public static IHandOverToNewSectionOptionsBuilder ByRedacting(this IObfuscatorOptionsBuilder builder, string symmetricalAccoutrement)
        {
            return builder.With(new RedactedObfuscator(symmetricalAccoutrement));
        }
        
        /// <summary>
        /// Add an obfuscator that replaces a secret with an ornamented "REDACTED". 
        /// </summary>
        /// <param name="builder">The options builder.</param>
        /// <param name="leftAccoutrement">The string to adorn the left of the word "REDACTED".</param>
        /// <param name="rightAccoutrement">The string to adorn the right of the word "REDACTED".</param>
        /// <returns>The options builder</returns>
        public static IHandOverToNewSectionOptionsBuilder ByRedacting(this IObfuscatorOptionsBuilder builder, string leftAccoutrement, string rightAccoutrement)
        {
            return builder.With(new RedactedObfuscator(leftAccoutrement, rightAccoutrement));
        }

    }
}