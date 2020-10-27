using System.Text.RegularExpressions;

namespace Stravaig.Extensions.Configuration.Diagnostics.Matchers
{
    public class RegexMatcher : IMatcher
    {
        private readonly Regex _regex;
        public RegexMatcher(string regularExpression)
        {
            _regex = new Regex(regularExpression, RegexOptions.IgnoreCase);
        }

        public RegexMatcher(string regularExpression, RegexOptions options)
        {
            _regex = new Regex(regularExpression, options);
        }

        /// <inheritdoc />
        public bool IsMatch(string key)
        {
            return _regex.IsMatch(key);
        }
    }
}