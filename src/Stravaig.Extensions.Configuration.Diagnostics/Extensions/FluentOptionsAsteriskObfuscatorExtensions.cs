using Stravaig.Extensions.Configuration.Diagnostics.FluentOptions;
using Stravaig.Extensions.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics
{
    public static class FluentOptionsAsteriskObfuscatorExtensions
    {
        public static IHandOverToNewSectionOptionsBuilder WithAsterisks(this IObfuscatorOptionsBuilder builder)
        {
            return builder.With(new MatchedLengthAsteriskObfuscator());
        }

        public static IHandOverToNewSectionOptionsBuilder WithFixedAsterisks(this IObfuscatorOptionsBuilder builder)
        {
            return builder.With(new FixedAsteriskObfuscator());
        }

        public static IHandOverToNewSectionOptionsBuilder WithFixedAsterisks(this IObfuscatorOptionsBuilder builder, int numAsterisks)
        {
            return builder.With(new FixedAsteriskObfuscator(numAsterisks));
        }
    }
}