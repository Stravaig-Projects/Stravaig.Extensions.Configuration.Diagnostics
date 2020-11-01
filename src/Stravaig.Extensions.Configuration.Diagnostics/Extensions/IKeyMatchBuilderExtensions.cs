using System.Text.RegularExpressions;
using Stravaig.Extensions.Configuration.Diagnostics.Matchers;

namespace Stravaig.Extensions.Configuration.Diagnostics
{
    public static class IKeyMatchBuilderExtensions
    {
        public static IKeyMatchBuilder AddContainsMatcher(this IKeyMatchBuilder builder, string contains)
        {
            return builder.Add(new ContainsMatcher(contains));
        }

        public static IKeyMatchBuilder AddRegexMatcher(this IKeyMatchBuilder builder, string regularExpression)
        {
            return builder.Add(new RegexMatcher(regularExpression));
        }

        public static IKeyMatchBuilder AddRegexMatcher(this IKeyMatchBuilder builder, string regularExpression,
            RegexOptions options)
        {
            return builder.Add(new RegexMatcher(regularExpression, options));
        }
    }
}