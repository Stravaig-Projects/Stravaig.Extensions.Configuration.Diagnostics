using Stravaig.Extensions.Configuration.Diagnostics.FluentOptions;
using Stravaig.Extensions.Configuration.Diagnostics.Obfuscators;

namespace Stravaig.Extensions.Configuration.Diagnostics
{
    public static class FluentOptionsRedactingExtensions
    {
        public static IHandOverToNewSectionOptionsBuilder ByRedacting(this IObfuscatorOptionsBuilder builder)
        {
            return builder.With(new RedactedObfuscator());
        }

        public static IHandOverToNewSectionOptionsBuilder ByRedacting(this IObfuscatorOptionsBuilder builder, string symmetricalAccountrement)
        {
            return builder.With(new RedactedObfuscator(symmetricalAccountrement));
        }
        
        public static IHandOverToNewSectionOptionsBuilder ByRedacting(this IObfuscatorOptionsBuilder builder, string leftAccountrement, string rightAccoutrement)
        {
            return builder.With(new RedactedObfuscator(leftAccountrement, rightAccoutrement));
        }

    }
}