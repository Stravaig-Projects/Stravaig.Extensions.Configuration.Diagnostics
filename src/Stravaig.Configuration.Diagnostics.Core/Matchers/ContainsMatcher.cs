using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Stravaig.Configuration.Diagnostics.Matchers
{
    /// <summary>
    /// A matcher that effectively matches as <code>key.Contains(value)</code>.
    /// </summary>
    public class ContainsMatcher : IMatcher
    {
        private readonly ImmutableArray<string> _contains;
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
            : this(new[]{contains}, comparison)
        {
            if (string.IsNullOrWhiteSpace(contains))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(contains));
        }
        
        /// <summary>
        /// Initialises a matcher where the condition is that the given value is contained within the values being matched.
        /// </summary>
        /// <param name="contains">A set of values to match against.</param>
        /// <param name="comparison">The method of string comparison, default is OrdinalIgnoreCase</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ContainsMatcher(IEnumerable<string> contains, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (contains == null)
                throw new ArgumentNullException(nameof(contains));

            _contains = contains
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .ToImmutableArray();
            if (_contains.Length == 0)
                throw new ArgumentException("The collection must contain at least one valid value. All values are null, empty or whitespace.", nameof(contains));
            _comparison = comparison;
        }
        
        /// <inheritdoc />
        public bool IsMatch(string key)
        {
#if NETSTANDARD2_0
            // Based on https://github.com/microsoft/referencesource/blob/f461f1986ca4027720656a0c77bede9963e20b7e/mscorlib/system/string.cs#L2172
            return _contains.Any(contains => key.IndexOf(contains, _comparison) >= 0);
#else
            return _contains.Any(contains => key.Contains(contains, _comparison));
#endif
        }
    }
}