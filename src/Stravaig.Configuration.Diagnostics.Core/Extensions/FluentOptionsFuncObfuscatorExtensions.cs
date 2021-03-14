using System;
using Stravaig.Configuration.Diagnostics.FluentOptions;
using Stravaig.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Configuration.Diagnostics
{
    /// <summary>
    /// Extensions for the fluent configuration builder to add function based obfuscators
    /// </summary>
    public static class FluentOptionsFuncObfuscatorExtensions
    {
        /// <summary>
        /// Obfuscate with the given function.
        /// </summary>
        /// <param name="builder">The fluent configuration options builder.</param>
        /// <param name="obfuscatingFunction">The function to obfuscate the secret with.</param>
        /// <returns>the fluent configuration objects builder.</returns>
        public static IHandOverToNewSectionOptionsBuilder With(
            this IObfuscatorOptionsBuilder builder,
            Func<string, string> obfuscatingFunction)
        {
            return builder.With(new FuncObfuscator(obfuscatingFunction));
        }
    }
}