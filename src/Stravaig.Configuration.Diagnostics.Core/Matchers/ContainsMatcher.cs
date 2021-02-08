using System;

namespace Stravaig.Configuration.Diagnostics.Matchers
{
    /// <summary>
    /// A matcher that effectively matches as <code>key.Contains(value)</code>.
    /// </summary>
    public class ContainsMatcher : IMatcher
    {
        private readonly string _contains;
        private readonly StringComparison _comparison;

        /// <summary>
        /// Initialises a matcher where the condition is that the given value is contained within the value being matched.
        /// </summary>
        /// <param name="contains">The string that must be contained in the key being matched against.</param>
        public ContainsMatcher(string contains)
            :this(contains, StringComparison.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// Initialises a matcher where the condition is that the given value is contained within the value being matched.
        /// </summary>
        /// <param name="contains">The string that must be contained in the key being matched against.</param>
        /// <param name="comparison">The style of comparison.</param>
        public ContainsMatcher(string contains, StringComparison comparison)
        {
            if (string.IsNullOrWhiteSpace(contains))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(contains));
            _contains = contains;
            _comparison = comparison;
        }

        /// <inheritdoc />
        public bool IsMatch(string key)
        {
            // Based on https://github.com/microsoft/referencesource/blob/f461f1986ca4027720656a0c77bede9963e20b7e/mscorlib/system/string.cs#L2172
            return key.IndexOf(_contains, _comparison) >= 0;
        }
    }
}