using System;
using System.Text.RegularExpressions;

namespace Stravaig.Configuration.Diagnostics.Matchers
{
    /// <summary>
    /// A matcher that uses a regular expression to match to the given key.
    /// </summary>
    public class RegexMatcher : IMatcher
    {
        private readonly Regex _regex;

        /// <summary>
        /// Initialise a Regular Expression matcher that ignores case.
        /// </summary>
        /// <param name="regexPattern">The regular expression pattern.</param>
        public RegexMatcher(string regexPattern)
            : this (regexPattern, RegexOptions.IgnoreCase)
        {
        }

        /// <summary>
        /// Initialise a Regular Expression matcher.
        /// </summary>
        /// <param name="regexPattern">The regular expression pattern.</param>
        /// <param name="options">The options for how the regular expression is interpreted.</param>
        public RegexMatcher(string regexPattern, RegexOptions options)
        {
            if (string.IsNullOrWhiteSpace(regexPattern))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(regexPattern));

            _regex = new Regex(regexPattern, options);
        }

        /// <inheritdoc />
        public bool IsMatch(string key)
        {
            return _regex.IsMatch(key);
        }
    }
}